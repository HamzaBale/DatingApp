using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTO;
using API.Entities;
using API.Extensions;
using API.helpers;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class LikeRepository : ILikeRepository
    {
        private readonly DataContext _context;

        public LikeRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<UserLike> GetUserLike(int sourceUserId, int likedUserId) //restituisce la riga all'interno della tabella nel caso ci sia
        {
           return await _context.Likes.FindAsync(sourceUserId,likedUserId);// siccome la nostra chiave primaria all'interno
           //di Likes è la coppia sourceId, LikedUserId ==> questa ci restituisce subito l'oggetto.
           //io do in ingresso alla funzione due id e vedo se source ha messo mi piace a likedUser.
           //se mi restiuisce l'oggetto => allora si. altrimenti no.
           //utile per togliere mi piace
            
        }

        public  PageList<LikeDto> GetUserLikes(LikeParams likeparams )
        {
            var users = _context.Users.OrderBy(u => u.UserName).AsQueryable();
            var likes = _context.Likes.AsQueryable();
        
            if(likeparams.predicate =="source"){
                likes = likes.Where(like => like.SourceUserId == likeparams.userId);//n righe dove sourceid == userid

                users = likes.Select(like => like.LikedUser); //Select LikedUser from Likes Table.
                //restiuirà n righe fatte solo da LikedUser che ricordo essere di tipo AppUser.
            }
             if(likeparams.predicate =="likedby"){
                 likes = likes.Where(like => like.LikedUserId == likeparams.userId);//n righe dove sourceid == userid

                users = likes.Select(like => like.SourceUser); //select lista di user che gli piacciono.
            }
            var us =  users.Select(user=> new LikeDto{
                Username = user.UserName,
                KnownAs = user.KnownAs,
                Age = user.DateOfBirth.CalculateAge(),
                PhotoUrl = user.Photos.FirstOrDefault(p => p.IsMain).Url,
                Id = user.Id
            }).AsEnumerable();
            return PageList<LikeDto>.CreateAsync(us,likeparams.pageNumber,likeparams.pageSize);


       
           /* return await users.Select(user=> new LikeDto{
                Username = user.UserName,
                KnownAs = user.KnownAs,
                Age = user.DateOfBirth.CalculateAge(),
                PhotoUrl = user.Photos.FirstOrDefault(p => p.IsMain).Url,
                Id = user.Id
            }).ToListAsync(); //stessa cosa di sopra. Prenderò le righe ritornate dalla select trasformo in lista.*/

        }
         public UserLike DislikeUser(UserLike Disliked){

                return  _context.Likes.Remove(Disliked).Entity;
         }

        public async Task<AppUser> GetUserWithLikes(int userId) //return users that he liked
        {
            return await _context.Users.Include(x=> x.LikedUser) //return la lista presente in AppUser semplicemente
            .FirstOrDefaultAsync(x=> x.Id == userId);//o meglio, restiuisco L'user e ci includo la lista di quelli a cui ha messo mi piace
        }
    }
}