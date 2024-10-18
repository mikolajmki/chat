using Core.Models;

namespace Core.Repositories.ApiModels;

public sealed record LeaveChatRequest
{
    public User User { get; init; } = new();
}
