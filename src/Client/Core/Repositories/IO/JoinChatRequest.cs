using Core.Models;

namespace Core.Repositories.IO;

public sealed record JoinChatRequest
{
    public User User { get; init; } = new ();
}
