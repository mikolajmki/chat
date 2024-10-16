using Core.Models;

namespace Core.Repositories.IO;

public sealed record SendMessageRequest
{
    public Message Message { get; init; } = new();
}
