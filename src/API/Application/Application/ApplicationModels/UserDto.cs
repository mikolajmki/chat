namespace Application.ApplicationModels;

public sealed record UserDto
{
    public Guid Id { get; init; } = Guid.Empty;
    public string ConnectionId { get; private set; } = string.Empty;
    public string Name { get; init; } = string.Empty;

    public void SetConnectionId(string id)
    {
        ConnectionId = id;
    }
}
