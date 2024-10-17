using Application.Abstractions;
using Application.ApplicationModels;
using Application.Domain;
using Application.Validation;
using MapsterMapper;
using Microsoft.Extensions.Logging;

namespace Application.Services;

internal class UserService(
        IUserRepository _userRepository,
        IMapper _mapper,
        ILogger<UserService> _logger
    ) : IUserService
{
    public bool AddUserToChat(UserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);

        var userValidator = new UserValidator();
        var validationResult = userValidator.Validate(user);

        if (!validationResult.IsValid)
        {
            _logger.LogError("Invalid username {userName}", user.Name);
            return false;
        }

        if (_userRepository.IsExisting(user.Name))
        {
            var userId = _userRepository.GetUserIdByName(user.Name);

            if (_userRepository.IsActive(userId))
            {
                _logger.LogError("User with name {userName} already active", user.Name);
                return false;
            }

            _userRepository.Activate(userId);

            return true;
        }

        user.GenerateId();

        _userRepository.AddUser(user);

        return true;
    }

    public void DeactivateByConnectionId(string id) 
    {
        _userRepository.DeactivateByConnectionId(id);
    }

    public void RemoveFromChatByConnectionId(string id)
    {
        _userRepository.RemoveFromChatByConnectionId(id);
    }
}
