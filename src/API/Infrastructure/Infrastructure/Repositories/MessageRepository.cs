using Application.Abstractions;
using Application.Domain;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

internal class MessageRepository(
        DataContext _context
    ) : IMessageRepository
{
    public void AddMessage(Message message)
    {
        _context.Messages.Add(message);
    }

    public IEnumerable<Message> GetMessages()
    {
        var messages = _context.Messages;
        
        return messages;
    }
}
