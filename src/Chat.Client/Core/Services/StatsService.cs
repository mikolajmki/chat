using Core.Abstractions;
using Core.Models;

namespace Core.Services;

internal class StatsService(
        IStatsRepository _statsRepository
    ) : IStatsService
{
    public async Task<Stats> GetStats()
    {
        return await _statsRepository.GetStats();
    }
}
