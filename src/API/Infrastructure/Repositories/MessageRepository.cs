using Application.Abstractions;
using Application.Domain;
using Infrastructure.Data;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories;

internal class MessageRepository(
        ILogger<MessageRepository> _logger,
        DataContext _context
    ) : IMessageRepository
{
    public void AddMessage(Message message)
    {
        var user = _context.Users.Single(x => x.Name == message.GetUserName());

        message.GenerateId();
        message.SetUser(user);
        message.SetDateTimeNow();

        _context.Messages.Add(message);

        _logger.LogInformation(
            @"Message with content {messageContent} of user with name {userName} ", 
            message.Content,
            message.GetUserName()
        );
    }

    public Message GetLatestMessage()
    {
        var message = _context.Messages.MaxBy(x => x.CreatedAt);

        return message;
    }

    public IEnumerable<Message> GetMessages()
    {
        var messages = _context.Messages;
        
        return messages;
    }
}
