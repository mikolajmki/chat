using Core.Models;

namespace Core.Repositories.ApiModels;

public sealed record GetMessagesResponse
{
    public IEnumerable<Message> Messages { get; init; } = [];
}
