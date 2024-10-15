namespace Presentation.PresentationModels;

public sealed record StatsApiModel
{
    public IEnumerable<UserApiModel> ActiveUsers { get; init; } = [];
    public IEnumerable<UserApiModel> InActiveUsers { get; init; } = [];
    public int IsActiveCount { get; init; }
}
