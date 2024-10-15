using Application.ApplicationModels;

namespace Application.Services;

public interface IUserService
{
    bool AddUserToChat(UserDto user);
    void DeactivateUserByConnectionId(string id);
}
