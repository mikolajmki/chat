namespace Application.ApplicationModels;

public sealed record MessageDto
{
    public string UserId { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
}
