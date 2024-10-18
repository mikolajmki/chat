namespace Application.Domain;

public sealed record Message
{
    public Guid Id { get; private set; }
    public User User { get; private set; } = new ();
    public string Content { get; init; } = string.Empty;
    public DateTime CreatedAt { get; private set; }

    public void GenerateId()
    {
        Id = Guid.NewGuid();
    }

    public void SetCreatedAtNow()
    {
        CreatedAt = DateTime.Now;
    }

    public void SetCreatedAt(DateTime createdAt)
    {
        CreatedAt = createdAt;
    }

    public void SetId(Guid id)
    {
        Id = id;
    }

    public void SetUser(User user)
    {
        User = user;
    }

    public string GetUserName()
    {
        return User.Name;
    }
}
