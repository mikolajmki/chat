using Application.Abstractions;
using Application.ApplicationModels;
using MapsterMapper;

namespace Application.Services;

internal class MessageService(
        IMessageRepository _repository,
        IMapper _mapper
    ) : IMessageService
{
    public IEnumerable<MessageDto> GetMessages()
    {
        var messages = _repository.GetMessages();
        var messageDtos = _mapper.Map<IEnumerable<MessageDto>>(messages);

        return messageDtos;
    }

    public bool Send(SendMessageCommand command)
    {
        throw new NotImplementedException();
    }
}
