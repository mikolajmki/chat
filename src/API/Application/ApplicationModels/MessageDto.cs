namespace Application.ApplicationModels;

public sealed record MessageDto
{
    public Guid Id { get; private set; }
    public UserDto User { get; init; } = new ();
    public string Content { get; init; } = string.Empty;
    public DateTime CreatedAt { get; private set; }

}
