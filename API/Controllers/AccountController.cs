using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTO;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public AccountController(DataContext context, ITokenService tokenService) //caso di injectoon serve per comunicare con il databse
        {
            _context = context;
            _tokenService = tokenService;
        }
    [HttpPost("register")]
    public async Task<ActionResult<UserDTO>> register(RegisterDTO register){
            if(await this.IsPresent(register.name)) return BadRequest("Username already exists");  
            AppUser user = new AppUser();
            using var hmac = new HMACSHA512();
            user.UserName = register.name.ToLower();
            user.passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(register.password));
            user.passwordSalt = hmac.Key;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();


           return new UserDTO{
               userName = user.UserName,
               token = _tokenService.CreateToken(user)
           };

    }
   [HttpPost("login")]
    public async Task<ActionResult<UserDTO>> login(loginDTO login){
        if(!(await this.IsPresent(login.name))) return Unauthorized("Username is Invalid");

        var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == login.name.ToLower());
        
        using var hmac = new HMACSHA512(user.passwordSalt);
        byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.password));
        for(int i =0; i<user.passwordHash.Length;i++){
            if(passwordHash[i] != user.passwordHash[i]) return Unauthorized("Password not correct");
        }
 
           return new UserDTO{
               userName = user.UserName,
               token = _tokenService.CreateToken(user)
           };
           }
    

    private async Task<bool> IsPresent(string UserName){

        return await _context.Users.AnyAsync(x => x.UserName == UserName.ToLower());

    }

    }
}