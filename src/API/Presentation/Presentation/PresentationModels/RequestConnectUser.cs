namespace Presentation.PresentationModels;

public sealed record RequestConnectUser
{
    public UserApiModel User { get; init; } = new ();
}
