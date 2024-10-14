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
            _userRepository.Activate(userDto.Name);

            return true;
        }

        var user = _mapper.Map<User>(userDto);

        return _userRepository.AddUser(user);
    }
}
