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

namespace API.Controllers
{
    public class RolesController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _automapper;

        public RolesController(IUserRepository userRepository,UserManager<AppUser> userManager
        ,IMapper automapper)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _automapper = automapper;
        }

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



    }
}