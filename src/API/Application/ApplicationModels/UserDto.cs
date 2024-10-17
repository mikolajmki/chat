namespace Application.ApplicationModels;

public sealed record UserDto
{
    public Guid Id { get; private set; }
    public string ConnectionId { get; private set; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public bool IsActive { get; private set; }  

    public void SetConnectionId(string id)
    {
        ConnectionId = id;
    }
}
