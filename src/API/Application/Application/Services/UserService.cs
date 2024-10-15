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
            var userId = _userRepository.GetUserIdByName(userDto.Name);

            if (_userRepository.IsActive(userId))
            {
                return false;
            }

            _userRepository.Activate(userId);

            return true;
        }

        var user = _mapper.Map<User>(userDto);

        _userRepository.AddUser(user);

        return true;
    }

    public void DeactivateUserByConnectionId(string id) 
    {
        _userRepository.DeactivateByConnectionId(id);
    }
}
