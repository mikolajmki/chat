using Core.Models;

namespace Core.Repositories.IO;

public sealed record GetMessagesResponse
{
    public IEnumerable<Message> Messages { get; init; } = [];
}
