using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTO;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
   // [Route("api/[controller]")]
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _repo;
        private readonly IMapper _automapper;

        public AccountController(DataContext context, ITokenService tokenService, IUserRepository repo, IMapper autoMapper) //caso di injectoon serve per comunicare con il databse
        {
            _automapper = autoMapper;
            _context = context;
            _tokenService = tokenService;
            _repo = repo;
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> register(RegisterDTO register)
        {
            if (await this.IsPresent(register.username)) return BadRequest("Username already exists");
            AppUser user = new AppUser();
            using var hmac = new HMACSHA512();
            user.UserName = register.username.ToLower();
            user.passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(register.password));
            user.passwordSalt = hmac.Key; //Salt is important. It gets appended to the passwordHash and creates new Hash
            //in our case the salt is the key used to calculate the first hash

            _automapper.Map(register,user);
           /* user.KnownAs = register.knownAs;
            user.DateOfBirth = new DateTime().ToLocalTime();
            user.City = register.City;
            user.Country = register.country;*/
            _context.Users.Add(user);
            await _context.SaveChangesAsync();


            return new UserDTO
            {
                userName = user.UserName,
                token = _tokenService.CreateToken(user),
                //photoUrl = user.Photos.FirstOrDefault(x => x.IsMain).Url No need of photourl in here
                //when someone registers it does not have photos in it
            };

        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> login(loginDTO login)
        {
            if (!(await this.IsPresent(login.name))) return Unauthorized("Username is Invalid");
                
            var user = await _repo.GetUserByUsernameAsync(login.name);

            using var hmac = new HMACSHA512(user.passwordSalt);
            byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.password));
            for (int i = 0; i < user.passwordHash.Length; i++)
            {
                if (passwordHash[i] != user.passwordHash[i]) return Unauthorized("Password not correct");
            }

            return new UserDTO
            {
                userName = user.UserName,
                token = _tokenService.CreateToken(user),
                photoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url
            };
        }


        private async Task<bool> IsPresent(string UserName)
        {

            return await _context.Users.AnyAsync(x => x.UserName == UserName.ToLower());

        }

    }
}