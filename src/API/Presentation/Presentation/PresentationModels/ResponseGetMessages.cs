namespace Presentation.PresentationModels;

public sealed record ResponseGetMessages
{
    public IEnumerable<MessageApiModel> Messages { get; init; } = [];
}