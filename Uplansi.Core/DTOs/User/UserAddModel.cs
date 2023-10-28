namespace Uplansi.Core.DTOs.User;

public record UserAddModel
{
    public required string UserName { get; init; }
    public required string Password { get; init; }
    public string? FullName { get; init; }
    public required string DisplayName { get; init; }
    public string? Email { get; init; }
    public ICollection<string>? Roles { get; init; }

    public required string Language { get; init; }
    public required string Country { get; init; }
}