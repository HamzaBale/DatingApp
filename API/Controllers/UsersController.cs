using System.Collections.Generic;
using System.Linq;
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

    [HttpPost("update")]
       public async Task<ActionResult<memberDto>> UpdateUser(UpdateDto member){
           
        var user = await _repo.GetUserByUsernameAsync(member.OldUsername);
        if(member.username != "") user.UserName = member.username;
        if(member.introduction != "") user.Introduction = member.introduction;
        var membertemp = _autoMapper.Map<memberDto>(user);
        return membertemp;
            }
    }
}