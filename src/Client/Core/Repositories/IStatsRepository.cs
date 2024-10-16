using Core.Repositories.IO;

namespace Application.Abstractions;

public interface IStatsRepository
{
    Task<GetStatsResponse> GetStats();
}
