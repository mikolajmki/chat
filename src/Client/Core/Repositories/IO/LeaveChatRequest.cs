using Core.Models;

namespace Core.Repositories.IO;

public sealed record LeaveChatRequest
{
    public User User { get; init; } = new();
}
