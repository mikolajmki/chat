using Application.Abstractions;
using Application.Domain;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

internal class MessageRepository(
        IDataContext _context
    ) : IMessageRepository
{
    public bool AddMessage(Message message)
    {
        var count = _context.Messages.Count();

        _ = _context.Messages.Append(message);

        var newCount = _context.Messages.Count();

        return newCount > count;
    }

    public IEnumerable<Message> GetMessages()
    {
        var messages = _context.Messages;
        
        return messages;
    }
}
