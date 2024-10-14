using Application.ApplicationModels;

namespace Application.Services;

public interface IMessageService
{
    bool SendMessage(SendMessageCommand command);
    IEnumerable<MessageDto> GetMessages();
}