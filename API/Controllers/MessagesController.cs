using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.DTO;
using API.Entities;
using API.Extensions;
using API.helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class MessagesController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _automapper;

        public MessagesController(DataContext context, IMessageRepository messageRepository,
        IUserRepository userRepository, IMapper autoMapper )
        {
            _context = context;
            _messageRepository = messageRepository;
            _userRepository = userRepository;
            _automapper = autoMapper;
        }

        [HttpPost]
        public async Task<ActionResult<MessageDto>> CreateMessage([FromBody]CreateMessageDto createMessage){
            var message = new Message();
            message.SenderUsername = User.GetUsername();
            if(message.SenderUsername == createMessage.RecipientUsername) return BadRequest("Cannot Send messages to yourself");
            if(createMessage == null) return BadRequest("Send parameters, cuz this are null");
            if(string.IsNullOrEmpty(createMessage.Content))return BadRequest("can't send void message");
            message.RecipientUsername = createMessage.RecipientUsername;
            message.Content = createMessage.Content;
            message.Sender = await _userRepository.GetUserByUsernameAsync(message.SenderUsername);
            message.Recipient =  await _userRepository.GetUserByUsernameAsync(message.RecipientUsername);
            if(message.Recipient == null) return NotFound();
            _messageRepository.AddMessage(message);
            if(await _messageRepository.SaveAllAsync())   return _automapper.Map<MessageDto>(message);
            
            return BadRequest("Something went Wrong");
        }

        [HttpGet]
         public ActionResult<PageList<MessageDto>> GetMessagesByUser([FromQuery]MessageParams messageParams){
                
            messageParams.Username = User.GetUsername();

           var message =  _messageRepository.GetMessagesForUser(messageParams);

            Response.addPaginationHeader(message.TotalPages,message.TotalCount,message.CurrentPage,message.PageSize);
            return message;

         }
        
        [HttpGet("{recipient}")]
        public async  Task<ActionResult<IEnumerable<MessageDto>>> GetUsersConversation(string recipient){
                
            var username = User.GetUsername();
            var userRecp = await _context.Users.FirstOrDefaultAsync(x=> x.UserName == recipient);
            if(userRecp == null) return NotFound("user not found");
            return Ok(_messageRepository.GetMessageThread(username,recipient));

         }
        
        [HttpDelete("{messageId}")]
         public async Task<ActionResult<MessageDto>> DeleteMessage(int messageId){

                var message = await _messageRepository.GetMessage(messageId);
                if(message == null) return BadRequest("no message with this id");
                if(message.SenderUsername.ToLower() != User.GetUsername().ToLower() && 
                    message.RecipientUsername.ToLower() != User.GetUsername().ToLower()) return Unauthorized("Who's You???");
                if(User.GetUsername().ToLower() == message.SenderUsername){ 
                    message.SenderDeleted = true;
                if(message.DateRead == null || message.RecipientDeleted == true)
                 _messageRepository.DeleteMessage(message); 
                }
                else if(User.GetUsername().ToLower() == message.RecipientUsername) {
                    message.RecipientDeleted = true;
                 if(message.SenderDeleted == true) _messageRepository.DeleteMessage(message); 
                }
                
                
                await _messageRepository.SaveAllAsync();
                return _automapper.Map<MessageDto>(message);
         }
        
        




    }
}