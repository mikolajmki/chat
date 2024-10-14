using Application.Domain;

namespace Application.Abstractions;

public interface IMessageRepository
{
    void AddMessage(Message message);
    IEnumerable<Message> GetMessages();
}
