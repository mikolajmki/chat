using Application.Abstractions;
using Application.ApplicationModels;
using Application.Domain;
using Application.Validation;
using MapsterMapper;

namespace Application.Services;

internal class MessageService(
        IMessageRepository _messageRepository,
        IMapper _mapper
    ) : IMessageService
{
    public MessageDto GetLatestMessage()
    {
        var message = _messageRepository.GetLatestMessage();
        var messageDto = _mapper.Map<MessageDto>(message);

        return messageDto;
    }

    public IEnumerable<MessageDto> GetMessages()
    {
        var messages = _messageRepository.GetMessages();

        var messageDtos = _mapper.Map<IEnumerable<MessageDto>>(messages);

        return messageDtos;
    }

    public bool SendMessage(MessageDto messageDto)
    {
        var message = _mapper.Map<Message>(messageDto);

        var messageValidator = new MessageValidator();
        var validationResult = messageValidator.Validate(message);

        if (validationResult.IsValid)
        {
            _messageRepository.AddMessage(message);

            return true;
        }

        return false;
    }
}
