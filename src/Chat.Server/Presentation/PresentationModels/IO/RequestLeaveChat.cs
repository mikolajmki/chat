namespace Presentation.PresentationModels.IO;

public sealed record RequestLeaveChat
{
    public UserApiModel User { get; init; } = new();

    public string GetUserConnectionId()
    {
        return User.ConnectionId;
    }
}
