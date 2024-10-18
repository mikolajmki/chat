namespace Core.Models;

public sealed record Stats
{
    public IEnumerable<User> ActiveUsers { get; init; } = [];
    public IEnumerable<User> InActiveUsers { get; init; } = [];
    public int IsActiveCount { get; init; }
}
