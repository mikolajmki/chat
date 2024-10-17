namespace Presentation.PresentationModels;

public sealed record UserApiModel
{
    public Guid Id { get; private set; }
    public string Name { get; init; } = string.Empty;
    public string ConnectionId { get; init; } = string.Empty;
    public bool IsActive { get; private set; } = true;
}
