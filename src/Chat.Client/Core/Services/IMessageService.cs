using Core.Models;

namespace Core.Services;

public interface IMessageService
{
    Task<bool> SendMessage(Message message);
    Task<Message> GetLatestMessage();
    Task<IEnumerable<Message>> GetMessages();
}
