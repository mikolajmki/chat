namespace Application.ApplicationModels;

public sealed record StatsDto
{
    public IEnumerable<UserDto> ActiveUsers { get; init; } = [];
    public IEnumerable<UserDto> InActiveUsers { get; init; } = [];
    public int IsActiveCount { get; init; }
}
