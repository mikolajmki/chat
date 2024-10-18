using Core.Models;

namespace Core.Services;

public interface IUserService
{
    Task<bool> JoinChat(User user);
    Task LeaveChat(User user);
}
