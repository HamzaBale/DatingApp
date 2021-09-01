using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTO;
using API.Entities;
using API.helpers;

namespace API.Interfaces
{
    public interface IMessageRepository
    {
        
        public void AddMessage(Message message);

        public void DeleteMessage(Message message);

        public Task<Message> GetMessage(int id);

        public Task<bool> SaveAllAsync();

         public PageList<MessageDto> GetMessagesForUser(MessageParams messageParams);
 
        public Task<IEnumerable<MessageDto>> GetMessageThread(string CurrentUsername, string RecipientUsername);
      
        


    }
}