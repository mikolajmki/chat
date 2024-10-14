namespace Application.Domain;

public sealed record User
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; init; } = string.Empty;
    public bool IsActive { get; init; } = true;
}
