using Application.Domain;

namespace Application.Abstractions;

public interface IMessageRepository
{
    void AddMessage(Message message);
    Message GetLatestMessage();
    IEnumerable<Message> GetMessages();
}
