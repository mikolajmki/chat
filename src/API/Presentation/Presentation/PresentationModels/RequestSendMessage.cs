namespace Presentation.Models;

public sealed record RequestSendMessage
{
    public string UserName { get; set; }
    public string MessageContent { get; init; } = string.Empty;
}