
using Core.Models;

namespace Core.Abstractions;

public interface IMessageRepository
{
    Task<bool> SendMessage(Message message);
    Task<Message> GetLatestMessage();
    Task<IEnumerable<Message>> GetMessages();
}
