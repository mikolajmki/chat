using Application.Domain;

namespace Application.Abstractions;

public interface IStatsRepository
{
    Stats GetStats();
}
