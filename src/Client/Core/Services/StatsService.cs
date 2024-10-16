using Application.Abstractions;
using Core.Models;

namespace Core.Services;

internal class StatsService(
        IStatsRepository _statsRepository
    ) : IStatsService
{
    public async Task<Stats> GetStats()
    {
        var response = await _statsRepository.GetStats();
        var stats = response.Stats;

        return stats;
    }
}
