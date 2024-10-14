using Application.Abstractions;
using Application.ApplicationModels;
using Application.Domain;
using MapsterMapper;

namespace Application.Services;

internal class MessageService(
        IUserService _userService,
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

    public bool SendMessage(SendMessageCommand command)
    {
        var userDto = new UserDto
        {
            Name = command.UserName,
        };

        if (!_userService.AddUserToChat(userDto))
        {
            return false;
        }

        var userId = _userService.GetIdByUserName(userDto.Name);

        var message = new Message
        {
            Content = command.MessageContent,
            UserId = userId
        };

        _repository.AddMessage(message);

        return true;
    }
}
