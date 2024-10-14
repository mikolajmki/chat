using Application.ApplicationModels;

namespace Application.Services;

public interface IMessageService
{
    bool Send(SendMessageCommand command);
    IEnumerable<MessageDto> GetMessages();
}