using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class LikesController : BaseApiController
    {
        private readonly ILikeRepository _likesRepository;
        private readonly IUserRepository _userRepository;

        public LikesController(ILikeRepository likesRepository, IUserRepository userRepository)
        {
            _likesRepository = likesRepository;
            _userRepository = userRepository;
        }

        
        [HttpPost("{username}")]
        public async Task<ActionResult> AddLike(string username){
            
            var sourceName = User.GetUsername();
            var sourceUser =await _likesRepository.GetUserWithLikes(User.GetUserId());
            var likedUser = await _userRepository.GetUserByUsernameAsync(username);
            if(likedUser == null) return BadRequest("who is "+ username);
            if(sourceUser == null) return BadRequest("who is "+ sourceName);
            var likes = await  _likesRepository.GetUserLike(sourceUser.Id, likedUser.Id);
           
            if(likes != null) return BadRequest("you already liked this user");
            if(username == sourceName) return BadRequest("you can't like yourself in my App");
            likes = new UserLike{
                LikedUserId = likedUser.Id,
                SourceUserId=sourceUser.Id
            };
            sourceUser.LikedUser.Add(likes);

            if(await _userRepository.SaveAllAsync()) return Ok("User Is Liked");
            
            return BadRequest("Error occured");

        }
        [HttpGet]
        public async  Task<ActionResult<IEnumerable<AppUser>>> GetLikedUsers(string predicate){
                 if(predicate != "likedby" && predicate != "source") return BadRequest("predicate should be source or likedby");
            var users = await _likesRepository.GetUserLikes(predicate,User.GetUserId());
            return Ok(users);

        }

    }
}