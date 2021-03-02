using PosApiJwt.Models;
using PosApiJwt.Interfaces;
using PosApiJwt.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace PosApiJwt.Services
{
    public class MessageService : IMessageService
    {
        private readonly MessagesDbContext messagesDbContext;

        public MessageService(MessagesDbContext messagesDbContext)
        {
            this.messagesDbContext = messagesDbContext;
        }
        public async Task<List<Message>> ReadMessages(string userName)
        {
            var messages = await messagesDbContext.Messages.Where(m => m.To == userName).ToListAsync();

            return messages;
        }
        public async Task<Message> ReadMessage(string userName, int messageId)
        {
            var message = await messagesDbContext.Messages.Where(m => m.MessageId == messageId && m.To == userName).FirstOrDefaultAsync<Message>();
            if (message != null)
            {
                messagesDbContext.Entry(message).Reference(s => s.MsgBody).Load();
            }

            return message;
        }
        public async Task<Message> CreateMessage(Message message)
        {
            await messagesDbContext.AddAsync(message);
            await messagesDbContext.SaveChangesAsync();

            return message;
        }
        public async Task<Message> UpdateMessage(Message message)
        {
            var msg = await messagesDbContext.Messages.Where(m => m.MessageId == message.MessageId).FirstOrDefaultAsync<Message>();
            await messagesDbContext.Entry(msg).Reference(s => s.MsgBody).LoadAsync();
            msg.Copy(message);
            messagesDbContext.Entry(msg).State = EntityState.Modified;
            await messagesDbContext.SaveChangesAsync();

            return msg;
        }
        public async Task DeleteMessage(int messageId)
        {
            var msg = await messagesDbContext.Messages.Where(m => m.MessageId == messageId).FirstOrDefaultAsync<Message>();
            await messagesDbContext.Entry(msg).Reference(s => s.MsgBody).LoadAsync();
            messagesDbContext.Messages.Remove(msg);
            await messagesDbContext.SaveChangesAsync();
        }
    }
}
