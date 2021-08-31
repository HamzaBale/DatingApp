using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTO;
using API.Entities;
using API.helpers;

namespace API.Interfaces
{
    public interface ILikeRepository
    {
        
        //secondo me 
        /*
        Metodo che restituisce la lista degli id utenti a cui ho messo mi piace a partire dal sourceId.
        public List<UserLike> GetLikedUsersById(int sourceId)

        Metodo che restituisce la lista degli id utenti che hanno messo mi piace a partire dal sourceId.
        public List<UserLike> GetLikedByUsersById(int likedId)
         _context.UserLike.Select(x => x.LikedId == likedId);   
        */

         Task<UserLike> GetUserLike(int sourceUserId, int likedUserId);
        Task<AppUser> GetUserWithLikes(int userId);
        PageList<LikeDto> GetUserLikes(LikeParams likeparams);
        UserLike DislikeUser(UserLike Disliked);

    }
}