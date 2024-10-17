using Core.Models;
using Core.Repositories.IO;

namespace Core.Repositories;

public interface IUserRepository
{
    Task<bool> JoinChat(JoinChatRequest request);
    Task LeaveChat(LeaveChatRequest request);
}
