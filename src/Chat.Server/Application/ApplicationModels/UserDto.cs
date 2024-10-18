namespace Application.ApplicationModels;

public sealed record UserDto
{
    public Guid Id { get; init; }
    public string ConnectionId { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public bool IsActive { get; private set; }
}
