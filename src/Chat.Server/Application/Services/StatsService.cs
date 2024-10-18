using Application.Abstractions;
using Application.ApplicationModels;
using MapsterMapper;

namespace Application.Services;

internal class StatsService(
        IStatsRepository _statsRepository,
        IMapper _mapper
    ) : IStatsService
{
    public StatsDto GetStats()
    {
        var stats = _statsRepository.GetStats();
        var statsDto = _mapper.Map<StatsDto>(stats);

        return statsDto;
    }
}
