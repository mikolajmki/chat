namespace Presentation.PresentationModels.IO;

public sealed record ResponseGetStats
{
    public StatsApiModel Stats { get; init; } = new ();
}
