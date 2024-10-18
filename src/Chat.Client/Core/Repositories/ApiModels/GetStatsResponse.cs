using Core.Models;

namespace Core.Repositories.ApiModels;

public sealed record GetStatsResponse
{
    public Stats Stats { get; init; } = new();
}
