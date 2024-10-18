namespace Presentation.PresentationModels.IO;

public sealed record RequestJoinChat
{
    public UserApiModel User { get; init; } = new();
}
