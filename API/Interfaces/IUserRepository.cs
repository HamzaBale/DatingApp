using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTO;
using API.Entities;
using API.helpers;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        
        void Update(AppUser user);
        Task<bool> SaveAllAsync();

        Task<PageList<memberDto>> GetAllUsers(UserParams userParams);
        
        Task<AppUser> GetUserByIdAsync(int id);

        Task<AppUser> GetUserByUsernameAsync(string userName);

        Task<IEnumerable<AppUser>> GetMembersAsync();

    }
}