using Application.Abstractions;
using Application.ApplicationModels;
using Application.Domain;
using MapsterMapper;

namespace Application.Services;

internal class MessageService(
        IMessageRepository _messageRepository,
        IMapper _mapper
    ) : IMessageService
{
    public IEnumerable<MessageDto> GetMessages()
    {
        var messages = _messageRepository.GetMessages();

        var messageDtos = _mapper.Map<IEnumerable<MessageDto>>(messages);

        return messageDtos;
    }

    public void SendMessage(MessageDto messageDto)
    {
        var message = _mapper.Map<Message>(messageDto);

        _messageRepository.AddMessage(message);
    }
}
