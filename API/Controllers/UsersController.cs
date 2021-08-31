using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Data;
using API.DTO;
using API.Entities;
using API.Extensions;
using API.helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    //[Route("api/[controller]")]
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _autoMapper;
        private readonly ICloudinary _photoservice;
        private readonly DataContext _context;
        public UsersController(IUserRepository repo, IMapper autoMapper, ICloudinary cloudinary,DataContext context) //caso di injection serve per comunicare con il databse
        {
            _photoservice = cloudinary;

            _repo = repo;
            _autoMapper = autoMapper;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<memberDto>>> GetUsersAsync([FromQuery]UserParams queryParams)
        {   
            var user = await _repo.GetUserByUsernameAsync(User.GetUsername());

            if(string.IsNullOrEmpty(queryParams.Gender)){ 
                queryParams.Gender = user.Gender == "male" ?  "female" :  "male";
            } 
    
            queryParams.CurrentUsername = User.GetUsername();
            var users = await _repo.GetAllUsers(queryParams);

            Response.addPaginationHeader(users.TotalPages,users.TotalCount,users.CurrentPage, users.PageSize);


            

            return Ok(users);
            //return Ok(await _repo.GetAllUsers()); //va messa in un Ok(Badrequest, Unauthorized) result affinchè sia una risposta http.
            //ActionResult<List<Appuser>> != List<AppUser>


        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<memberDto>> GetUser(int id)
        {

            var user = await _repo.GetUserByIdAsync(id);
            var photos = user.Photos;
            var membertemp = _autoMapper.Map<memberDto>(user);
            return membertemp;
        }

        [HttpGet("{username:alpha}", Name ="GetUser")]
        public async Task<ActionResult<memberDto>> GetUserByName(string username)
        {
            var user = await _repo.GetUserByUsernameAsync(username);
            var membertemp = _autoMapper.Map<memberDto>(user);
            return membertemp;
        }


        [HttpPut] //update risorsa gia esistente, Post invece quando creo nuova risorsa
        public async Task<ActionResult> UpdateUserPut(UpdateDto member)
        {
            var username = User.GetUsername(); //prende lo username del user connesso con il server
                                                //Attraverso il TOKEN inviato insieme alla risposta http.
            var user = await _repo.GetUserByUsernameAsync(username);
            _autoMapper.Map(member, user);
            _repo.Update(user);
            if (await _repo.SaveAllAsync()) return NoContent();

            return BadRequest("Something went wrong");

        }
        [HttpPost("add-photo")]
           public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile File){
                /*
                RICORDA PRENDERE USERNAME DA CLAIMS, NON ASPETTARLO NELLA RICHIESTA
                */
                var username = User.GetUsername();
                var user = await _repo.GetUserByUsernameAsync(username);
                var result = await _photoservice.AddPhotoAsync(File); 

                if(result.Error != null) return BadRequest(result.Error);

                var photo = new Photo{
                Url = result?.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
                 };

                if(user.Photos.Count <= 0){
                    photo.IsMain = true;
                }
                else photo.IsMain = false;
                user.Photos.Add(photo);
                 _repo.Update(user);
                if(await _repo.SaveAllAsync()) {
                   return  CreatedAtRoute("GetUser",new {username = user.UserName},_autoMapper.Map<PhotoDto>(photo));
                }
                //CraeteAtRoute serve a restituire una risposta più completa al client. in questo caso abbiamo
                //creato una nuova foto e aggiunta al database, il client deve sapere dove poter trovare la nuova 
                //immagine. Nell'header troverò "localhost:5001/users/nomeutente"

                return BadRequest("photo Problem");
            }

            [HttpPut("main-photo/{photoId}")]
            public async Task<ActionResult> SetPhotoMain(int photoId){

                var user = await _repo.GetUserByUsernameAsync(User.GetUsername());


                if(photoId <= 0 ) return BadRequest("send photoid correctly");
                
                var photo =  user.Photos.FirstOrDefault(x => x.Id == photoId);
                var currentmain = user.Photos.FirstOrDefault(x => x.IsMain);
                if(currentmain != null) currentmain.IsMain = false;
                photo.IsMain = true;
                await _repo.SaveAllAsync();
                return NoContent();
           
            }
            
            [HttpDelete("{photoId}")]
            public async Task<ActionResult<bool>> DeletePhotoById(int photoId){

                var user = await _repo.GetUserByUsernameAsync(User.GetUsername());
                var photo  = user.Photos.FirstOrDefault(x=> x.Id == photoId);
                user.Photos.Remove(photo);
                if(photo == null) return BadRequest("photo not existing");
                if(photo?.IsMain == true) user.Photos.FirstOrDefault(x=> x.Id != photoId).IsMain = true;
                 return  await _repo.SaveAllAsync();

            }

    }
}