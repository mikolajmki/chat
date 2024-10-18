using Core.Models;

namespace Core.Abstractions;

public interface IUserRepository
{
    Task<bool> JoinChat(User user);
    Task LeaveChat(User user);
}
