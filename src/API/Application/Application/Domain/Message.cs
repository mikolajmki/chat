namespace Application.Domain;

public sealed record Message
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public Guid UserId { get; init; } = Guid.Empty;
    public string Content { get; init; } = string.Empty;
}
