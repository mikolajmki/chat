using Application.ApplicationModels;

namespace Application.Services;

public interface IMessageService
{
    bool Send(MessageDto messageDto);
    IEnumerable<MessageDto> GetMessages();
}