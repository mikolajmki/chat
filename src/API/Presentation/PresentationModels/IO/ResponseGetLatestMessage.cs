namespace Presentation.PresentationModels.IO;

public sealed record ResponseGetLatestMessage
{
    public MessageApiModel LatestMessage { get; init; } = new ();
}
