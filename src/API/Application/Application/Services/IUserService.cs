using Application.ApplicationModels;

namespace Application.Services;

internal interface IUserService
{
    bool AddUser(UserDto user);
    bool IsExisting(UserDto user);
}
