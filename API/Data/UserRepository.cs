

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using API.DTO;
using API.Entities;
using API.Extensions;
using API.helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

           private readonly IMapper _autoMapper;

        public UserRepository(DataContext context, IMapper autoMapper)
        {
            _context = context;
            _autoMapper = autoMapper;
        }

        public async Task<PageList<memberDto>> GetAllUsers(UserParams userParams)
        {   
            DateTime Mindob;
            DateTime Maxdob;
            var request =  _context.Users.Include(p => p.Photos).AsQueryable();
              request = request.Where(x => x.Gender == userParams.Gender && !(x.UserName.Equals(userParams.CurrentUsername)));
     
             Mindob = DateTime.Today.AddYears(-userParams.FromAge - 1);
             Maxdob = DateTime.Today.AddYears(-userParams.ToAge);
             request = request.Where(x=> DateTime.Compare(x.DateOfBirth, Mindob) < 0 && DateTime.Compare(x.DateOfBirth, Maxdob) > 0);
             var tempMembers = _autoMapper.Map<IEnumerable<memberDto>>(request);
           
        
            /*request = request.Where(u => u.UserName != userParams.CurrentUsername && u.Gender == userParams.Gender);
            var Mindob = DateTime.Today.AddYears(-userParams.MaxAge -1);
            var Maxdob = DateTime.Today.AddYears(-userParams.MinAge);
            request = request.Where(u => (u.DateOfBirth <= Maxdob && u.DateOfBirth > Mindob));
            var  count = request.ToList().Count();
            var userobj = new GetUserObject();
            userobj.results = await request.Skip((userParams.pageNumber - 1)*userParams.pageSize) //pagination but not efficent
            .Take(userParams.pageSize).ToListAsync();
            userobj.count = count;*/

            return await  PageList<memberDto>.CreateAsync(tempMembers,userParams.pageNumber,userParams.pageSize);
        }

        public Task<IEnumerable<memberDto>> GetMembersAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(us => us.Id == id);
        }

        public async Task<AppUser> GetUserByUsernameAsync(string userName)
        {
            return await _context.Users.Include(x => x.Photos).
            FirstOrDefaultAsync(user => user.UserName == userName.ToLower());
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
     
             _context.Entry(user).State = EntityState.Modified;
        }
    }
}