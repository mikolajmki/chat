using Core.Models;

namespace Core.Services;

public interface IMessageService
{
    Task SendMessage(Message message);
    Task<Message> GetLatestMessage();
    Task<IEnumerable<Message>> GetMessages();
}
