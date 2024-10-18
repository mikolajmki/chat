using Application.ApplicationModels;

namespace Application.Services;

public interface IMessageService
{
    bool SendMessage(MessageDto command);
    IEnumerable<MessageDto> GetMessages();
    MessageDto GetLatestMessage();
}