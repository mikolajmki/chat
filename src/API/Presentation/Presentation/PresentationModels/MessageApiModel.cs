namespace Presentation.PresentationModels;

public sealed record MessageApiModel
{
    public UserApiModel User { get; init; } = new ();
    public string Content { get; init; } = string.Empty;
}
