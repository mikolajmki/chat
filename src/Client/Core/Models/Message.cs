namespace Core.Models;

public sealed record Message
{
    public User User { get; init; } = new ();
    public string Content { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
}
