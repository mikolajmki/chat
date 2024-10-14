namespace Presentation.Models;

sealed record class RequestSendMessage
{
    public string UserId { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;
}