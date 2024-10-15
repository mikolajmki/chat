using Application.ApplicationModels;

namespace Application.Services;

public interface IStatsService
{
    StatsDto GetStats();
}
