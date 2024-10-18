using Application.Abstractions;
using Application.Domain;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

internal class StatsRepository(
        DataContext _context
    ) : IStatsRepository
{
    public Stats GetStats()
    {
        var activeUsers = _context.Users.Where(x => x.IsActive);
        var inActiveUsers = _context.Users.Where(x => !x.IsActive);
        var isActiveCount = _context.Users.Count(x => x.IsActive);

        var stats = new Stats
        {
            ActiveUsers = activeUsers,
            InActiveUsers = inActiveUsers,
            IsActiveCount = isActiveCount
        };

        return stats;
    }
}
