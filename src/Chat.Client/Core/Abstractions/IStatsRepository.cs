using Core.Models;

namespace Core.Abstractions;

public interface IStatsRepository
{
    Task<Stats> GetStats();
}
