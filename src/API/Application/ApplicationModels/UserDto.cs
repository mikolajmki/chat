namespace Application.ApplicationModels;

public sealed record UserDto
{
    public string Id { get; init; } = string.Empty;
    public string ConnectionId { get; private set; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public bool IsActive { get; init; }

    public void SetConnectionId(string id)
    {
        ConnectionId = id;
    }
}
