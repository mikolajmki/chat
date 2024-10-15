namespace Presentation.PresentationModels;

public sealed record UserApiModel
{
    public string Id { get; init; } = Guid.Empty.ToString();
    public string Name { get; init; } = string.Empty;
    public bool IsActive { get; init; } = true;
}
