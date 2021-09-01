using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTO;
using API.Entities;
using API.helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _automapper;

        public MessageRepository(DataContext context, IMapper automapper)
        {
            _context = context;
            _automapper = automapper;
        }

        public void AddMessage(Message message)
        {
            _context.Messages.Add(message);
          
        }

        public void DeleteMessage(Message message)
        {
             _context.Messages.Remove(message);
        }

        public async Task<Message> GetMessage(int id)
        {
            return await _context.Messages.FindAsync(id);
        }

        public  PageList<MessageDto> GetMessagesForUser(MessageParams messageParams)
        {
            var query = _context.Messages.OrderByDescending(x=> x.MessageSent)
            .Include(src => src.Sender).ThenInclude(src => src.Photos)
            .Include(src => src.Recipient).ThenInclude(src => src.Photos)
            .AsQueryable();
            query = messageParams.Container switch
            {
                "Inbox" => query.Where(u => u.Recipient.UserName == messageParams.Username && u.RecipientDeleted == false),
                "Outbox" => query.Where(u => u.Sender.UserName == messageParams.Username && u.SenderDeleted == false),
                _ => query.Where(u => u.Recipient.UserName ==
                    messageParams.Username && u.DateRead == null && u.RecipientDeleted == false)
            };

            var queryDto = _automapper.Map<IEnumerable<Message>,IEnumerable<MessageDto>>(query.AsEnumerable());

            var page = PageList<MessageDto>.CreateAsync(queryDto,messageParams.pageNumber,messageParams.pageSize);
            
            return page;
        }
        public async Task<IEnumerable<MessageDto>> GetMessageThread(string CurrentUsername, string RecipientUsername)
        {
           var query =  _context.Messages.OrderByDescending(x=>x.MessageSent).AsQueryable();
            var messages =await  query
            .Include(x => x.Sender).ThenInclude(p => p.Photos)
            .Include(x => x.Recipient).ThenInclude(p => p.Photos)
            .Where(message => message.SenderUsername.ToLower() == CurrentUsername.ToLower()  && message.RecipientUsername.ToLower()  == RecipientUsername.ToLower() 
            && message.RecipientDeleted == false||
            message.SenderUsername.ToLower() == RecipientUsername.ToLower() && message.RecipientUsername.ToLower() == CurrentUsername.ToLower() && message.SenderDeleted == false)
            .ToListAsync();
            //Select(*) from Messages Where message.senderusername = currentusername && message.RecipientUsername == RecipientUsername.
            //Errore stavo dicendo che il sender deve essere anche il recipient. Bisogna usare l'or.
            //cioè return tutti i messaggi con sender = l'utente loggato, e recipient l'utente da cui voglio vedere tutti i messaggi.

            var unreadMessages = messages.Where( x => x.DateRead == null && x.RecipientUsername.ToLower() == CurrentUsername.ToLower()).ToList();
            
            if(unreadMessages.Any()){
                foreach (var item in unreadMessages)
                {   
                    item.DateRead = DateTime.Now; //questo si rifletterà sulla riga nel database.
                    //puntatore. In messages cambieranno i valori di DateRead.
                    
                }
            }
            await _context.SaveChangesAsync();

           return  _automapper.Map<IEnumerable<MessageDto>>(messages);
           


        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}