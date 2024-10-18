using Core.Models;

namespace Core.Repositories.ApiModels;

public sealed record GetLatestMessageResponse
{
    public Message LatestMessage { get; init; } = new();

}
