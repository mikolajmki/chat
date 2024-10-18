using Core.Models;

namespace Core.Services;

public interface IStatsService
{
    Task<Stats> GetStats();
}
