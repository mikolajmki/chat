namespace Application.Domain;

public sealed record User
{
    public Guid Id { get; private set; } = Guid.Empty;
    public string Name { get; init; } = string.Empty;
    public string ConnectionId { get; private set; } = string.Empty;
    public bool IsActive { get; private set; } = true;

    public void GenerateId()
    {
        Id = Guid.NewGuid();
    }

    public void SetId(Guid id)
    {
        Id = id;
    }

    public void SetConnectionId(string connectionId)
    {
        ConnectionId = connectionId;
    }

    public void SetIsActive(bool isActive)
    {
        IsActive = isActive;
    }
}
