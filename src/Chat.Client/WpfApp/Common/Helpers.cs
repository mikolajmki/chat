namespace WpfApp.Common;

public static class Notification
{
    public const string GetStats = "GetStats";
    public const string NewMessage = "NewMessage";
}

public sealed record IsConnectedIndicator
{
    public string Text { get; private set; } = string.Empty;
    public string Color { get; private set; } = string.Empty;

    public IsConnectedIndicator(string text, string color)
    {
        Text = text;
        Color = color;
    }

    public static IsConnectedIndicator Connected()
    {
        return new IsConnectedIndicator("Connected", "#FF126F13");
    }
    public static IsConnectedIndicator Disconnected()
    {
        return new IsConnectedIndicator("Disconnected", "red");
    }
}
internal class Error
{
    public const string CantJoin = "User name must be between 3 to 16 characters and cannot be taken by connected user.";
    public const string CantSendMessage = "Message must be between 1 to 200 characters.";
}
