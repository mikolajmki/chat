namespace Application.ApplicationModels;

public sealed record UserDto
{
    public string Name { get; init; } = string.Empty;
    public bool IsActive { get; init; } = true;
}
