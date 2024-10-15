namespace Application.Domain;

public sealed record Message
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public User User { get; private set; } = new ();
    public string Content { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    public void SetUser(User user)
    {
        User = user;
    }

    public string GetUserName()
    {
        return User.Name;
    }
}
