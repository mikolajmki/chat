namespace Presentation.PresentationModels;

public sealed record MessageApiModel
{
    public Guid Id { get; init; }
    public UserApiModel User { get; init; } = new ();
    public string Content { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
}
