using Core.Models;

namespace Core.Repositories.ApiModels;

public sealed record JoinChatRequest
{
    public User User { get; init; } = new ();
}
