using Application.ApplicationModels;

namespace Application.Services;

public interface IMessageService
{
    void SendMessage(MessageDto command);
    IEnumerable<MessageDto> GetMessages();
}