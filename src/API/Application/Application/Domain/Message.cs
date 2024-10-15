namespace Application.Domain;

public sealed record Message
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public User User { get; init; } = new ();
    public string Content { get; init; } = string.Empty;
}
