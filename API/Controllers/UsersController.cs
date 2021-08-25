using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Data;
using API.DTO;
using API.Entities;
using API.helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _autoMapper;

        public UsersController( IUserRepository repo, IMapper autoMapper) //caso di injection serve per comunicare con il databse
        {
           
            _repo = repo;
            _autoMapper = autoMapper;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<memberDto>>> GetUsersAsync(string token){
            
            var users = await _repo.GetAllUsers();
     
            IEnumerable<memberDto> tempMembers = _autoMapper.Map<IEnumerable<memberDto>>(users);
              
           
            return Ok(tempMembers);
             //return Ok(await _repo.GetAllUsers()); //va messa in un Ok(Badrequest, Unauthorized) result affinchè sia una risposta http.
             //ActionResult<List<Appuser>> != List<AppUser>


        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<memberDto>> GetUser(int id){

            var user =  await _repo.GetUserByIdAsync(id);
                var photos = user.Photos;
            var membertemp = _autoMapper.Map<memberDto>(user);
            return membertemp;
        }
        
        [HttpGet("{username:alpha}")]
       public async Task<ActionResult<memberDto>> GetUserByName(string username){
        var user = await _repo.GetUserByUsernameAsync(username);
        var membertemp = _autoMapper.Map<memberDto>(user);
        return membertemp;
            }


    [HttpPut]
    public async Task<ActionResult> UpdateUserPut(UpdateDto member){
        var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; //prende lo username del user connesso con il server
        //Attraverso il TOKEN inviato insieme alla risposta http.
        var user = await _repo.GetUserByUsernameAsync(username);
         _autoMapper.Map(member, user);
        _repo.Update(user);
        if( await _repo.SaveAllAsync()) return NoContent();

        return BadRequest("Something went wrong");

    }
    }
}