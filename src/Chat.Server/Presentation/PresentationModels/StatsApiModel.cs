namespace Presentation.PresentationModels;

public sealed record StatsApiModel
{
    public List<UserApiModel> ActiveUsers { get; init; } = [];
    public List<UserApiModel> InActiveUsers { get; init; } = [];
    public int IsActiveCount { get; init; }
}
