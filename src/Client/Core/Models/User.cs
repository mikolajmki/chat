namespace Core.Models;

public sealed record User
{
    public string Id { get; private set; } = Guid.Empty.ToString();
    public string Name { get; init; } = string.Empty;
    public string ConnectionId { get; set; } = string.Empty;
    public bool IsActive { get; init; } = true;
}
