namespace Uplansi.Core.DTOs.User;

public record UserUpdateModel
{
    public string? FullName { get; init; }
    public required string DisplayName { get; init; }
    public string? Email { get; init; }
    public ICollection<string>? Roles { get; init; }

    public required string Language { get; init; }
    public required string Country { get; init; }
}