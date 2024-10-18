using Core.Abstractions;
using Core.Models;

namespace Core.Services;

internal class UserService(
        IUserRepository _userRepository
    ) : IUserService
{
    public async Task<bool> JoinChat(User user)
    {
        return await _userRepository.JoinChat(user);
    }

    public async Task LeaveChat(User user)
    {
        await _userRepository.LeaveChat(user);
    }
}
