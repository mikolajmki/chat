namespace Presentation.PresentationModels.IO;

public sealed record ResponseGetMessages
{
    public List<MessageApiModel> Messages { get; init; } = [];
}