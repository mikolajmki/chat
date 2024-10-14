using Application.Domain;

namespace Application.Abstractions;

public interface IMessageRepository
{
    bool AddMessage(Message message);
    IEnumerable<Message> GetMessages();
}
