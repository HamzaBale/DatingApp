using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using API.DTO;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class RolesController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _automapper;

        public RolesController(UserManager<AppUser> userManager
        ,IMapper automapper)
        {
  
            _userManager = userManager;
            _automapper = automapper;
        }

        [Authorize(policy:"RequiredAdminRole")]
        [HttpGet]
        public async Task<ActionResult> GetUsersWithRoles(){
            
                var users = await _userManager.Users.Include(x => x.UserRoles)
                .ThenInclude(x => x.Role)
                .Select(x => new {
                    x.Id,
                    username = x.UserName,
                    role = x.UserRoles.Select(x => x.Role.Name).ToList()
                }).ToListAsync();
                return Ok(users);
        }

        [Authorize(policy:"RequiredAdminRole")]
        [HttpPost("edit/{username}")]
        public async Task<ActionResult> EditUserRole(string username,[FromBody]EditRoleParams roleParams){


                if(string.IsNullOrEmpty(roleParams.Edit) 
                || string.IsNullOrEmpty(username) 
                || roleParams.role.Count < 0) return BadRequest("Give me some real params");

                var user = await _userManager.Users.Include(user=> user.UserRoles)
                .ThenInclude(role => role.Role).FirstOrDefaultAsync(x=>x.UserName == username);
                if(user == null) return BadRequest("can't find this user");

                 //var ExRole = user.UserRoles.Select(x => x.Role.Name); questo per me nel caso volessi togliere permessi che gia aveva.
            var res = new IdentityResult {};
             if(roleParams.Edit == "remove") {
                 return Ok(await _userManager.RemoveFromRolesAsync(user,roleParams.role));
           
             } 
            
             if(roleParams.Edit == "add") {
                 return Ok(await _userManager.AddToRolesAsync(user,roleParams.role));
             
            
                }
              
              
              

       
                return Ok(res);



        }





/*
        [Authorize(policy:"RequiredAdminRole")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserRoleDto>>> GetUsersWithRoles(){
                var users = await _userRepository.GetMembersAsync();

                //users = _automapper.Map<ICollection<memberDto>>(users);

                UserRoleDto[] userRoles = new UserRoleDto[users.Count()];

               
                int count = 0;
                foreach (var item in users)
                {    var userWithRole=new UserRoleDto{};
                    userWithRole.user = _automapper.Map<memberDto>(item);
                    userWithRole.role = await _userManager.GetRolesAsync(item);
                    userRoles[count] = userWithRole;
                    count++;
                }
    

              return userRoles;
                
        }

*/

    }
}