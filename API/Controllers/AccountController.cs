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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
   // [Route("api/[controller]")]
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _repo;
        private readonly IMapper _automapper;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager
        ,ITokenService tokenService, IUserRepository repo, IMapper autoMapper) //caso di injectoon serve per comunicare con il databse
        {
            _automapper = autoMapper;
   
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _repo = repo;
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> register(RegisterDTO register)
        {
            if (await this.IsPresent(register.username)) return BadRequest("Username already exists");
            AppUser user = new AppUser();
            user.UserName = register.username.ToLower();
            _automapper.Map(register,user); // mappa le informazioni prese dal parametro di tipo registerDto in 
            //AppUser e le salva nel database.
            //_context.Users.Add(user);   await _context.SaveChangesAsync();
            var result = await _userManager.CreateAsync(user,register.password);
            if(!result.Succeeded) return BadRequest("Error in Server Registration");

           var res = await _userManager.CreateAsync(user,"Member");
            if(!res.Succeeded) return BadRequest("Error in Server Registration");

            return new UserDTO
            {
                userName = user.UserName,
                token = await _tokenService.CreateToken(user),
                //photoUrl = user.Photos.FirstOrDefault(x => x.IsMain).Url No need of photourl in here
                //when someone registers it does not have photos in it
            };

        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> login(loginDTO login)
        {
            if (!(await this.IsPresent(login.name))) return Unauthorized("Username is Invalid");
                
            var user = await _repo.GetUserByUsernameAsync(login.name);

            var result = await _signInManager.CheckPasswordSignInAsync(user,login.password,false);

            if(!result.Succeeded)  return Unauthorized("Password is wrong");

            return new UserDTO
            {
                userName = user.UserName,
                token = await _tokenService.CreateToken(user),
                photoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url,
                knownAs = user.KnownAs,
                Gender = user.Gender
            };
        }


        private async Task<bool> IsPresent(string UserName)
        {

            return await _userManager.Users.AnyAsync(x => x.UserName == UserName.ToLower());

        }

    }
}