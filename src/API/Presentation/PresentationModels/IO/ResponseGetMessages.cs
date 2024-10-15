namespace Presentation.PresentationModels.IO;

public sealed record ResponseGetMessages
{
    public IEnumerable<MessageApiModel> Messages { get; init; } = [];
}