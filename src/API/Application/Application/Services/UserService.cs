using Application.Abstractions;
using Application.ApplicationModels;
using Application.Domain;
using MapsterMapper;

namespace Application.Services;

internal class UserService(
        IUserRepository _userRepository,
        IMapper _mapper
    ) : IUserService
{
    public bool AddUserToChat(UserDto userDto)
    {
        if (_userRepository.IsExisting(userDto.Name))
        {
            var userId = _userRepository.GetIdByName(userDto.Name);

            if (!_userRepository.IsActive(userId))
            {
                _userRepository.Activate(userDto.Name);
                return true;
            }

            return false;
        }

        var user = _mapper.Map<User>(userDto);

        _userRepository.AddUser(user);

        return true;
    }

    public Guid GetIdByUserName(string userName)
    {
        return _userRepository.GetIdByName(userName);
    }
}
