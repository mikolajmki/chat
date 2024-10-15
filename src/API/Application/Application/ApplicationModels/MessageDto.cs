namespace Application.ApplicationModels;

public sealed record MessageDto
{
    public UserDto User { get; init; } = new ();
    public string Content { get; init; } = string.Empty;
}
