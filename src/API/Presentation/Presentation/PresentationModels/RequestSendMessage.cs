namespace Presentation.PresentationModels;

public sealed record RequestSendMessage
{
    public MessageApiModel Message { get; init; } = new ();
}