namespace Presentation.PresentationModels.IO;

public sealed record RequestConnectUser
{
    public UserApiModel User { get; init; } = new();
}
