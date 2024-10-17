using Application.ApplicationModels;

namespace Application.Services;

public interface IUserService
{
    bool AddUserToChat(UserDto user);
    void DeactivateByConnectionId(string id);
    void RemoveFromChatByConnectionId(string id);
}
