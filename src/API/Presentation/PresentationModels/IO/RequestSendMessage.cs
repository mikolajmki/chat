namespace Presentation.PresentationModels.IO;

public sealed record RequestSendMessage
{
    public MessageApiModel Message { get; init; } = new();
}