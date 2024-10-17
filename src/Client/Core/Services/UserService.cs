using Core.Models;
using Core.Repositories;
using Core.Repositories.IO;

namespace Core.Services;

internal class UserService(
        IUserRepository _userRepository
    ) : IUserService
{
    public async Task<bool> JoinChat(User user)
    {
        var request = new JoinChatRequest { User = user };

        return await _userRepository.JoinChat(request);
    }

    public async Task LeaveChat(User user)
    {
        var request = new LeaveChatRequest { User = user };

        await _userRepository.LeaveChat(request);
    }
}
