using Core.Models;

namespace Core.Repositories.IO;

public sealed record GetLatestMessageResponse
{
    public Message LatestMessage { get; init; } = new();
}
