namespace Application.ApplicationModels;

public sealed record SendMessageCommand
{
    public string UserId { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;
}
