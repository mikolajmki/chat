namespace Presentation.PresentationModels;

public sealed record MessageApiModel
{
    public Guid Id { get; private set; }
    public UserApiModel User { get; init; } = new ();
    public string Content { get; init; } = string.Empty;
    public DateTime CreatedAt { get; private set; }
}
