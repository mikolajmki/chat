namespace Application.ApplicationModels;

public sealed record SendMessageCommand
{
    public string UserName { get; init; } = string.Empty;
    public string MessageContent { get; init; } = string.Empty;
}
