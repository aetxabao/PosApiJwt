using PosApiJwt.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PosApiJwt.Interfaces
{
    public interface IMessageService
    {
        Task<Message> CreateMessage(Message message);
        Task<List<Message>> ReadMessages(string userName);
        Task<Message> ReadMessage(string userName, int messageId);
        Task<Message> UpdateMessage(Message message);
        Task DeleteMessage(int messageId);
    }
}
