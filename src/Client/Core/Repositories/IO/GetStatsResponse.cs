using Core.Models;

namespace Core.Repositories.IO;

public sealed record GetStatsResponse
{
    public Stats Stats { get; init; } = new();
}
