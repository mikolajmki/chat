using Application.ApplicationModels;

namespace Application.Services;

internal interface IUserService
{
    bool AddUserToChat(UserDto user);

    Guid GetIdByUserName(string userName);
}
