using Core.Models;

namespace Core.Repositories.ApiModels;

public sealed record SendMessageRequest
{
    public Message Message { get; init; } = new();
}
