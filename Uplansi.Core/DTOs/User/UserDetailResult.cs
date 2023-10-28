namespace Uplansi.Core.DTOs.User;

public record UserDetailResult
{
    public Guid Id { get; init; }
    public required string UserName { get; init; }
    public string? FullName { get; init; }
    public required string DisplayName { get; init; }
    public string? Gender { get; init; }
    public required string Country { get; init; }
    public required string Language { get; init; }
    public string? Email { get; init; }
    public string? PhoneNumber { get; init; }
    public List<string>? Roles { get; set; }
    public required DateTime CreatedAt { get; init; }
    public required KeyValueDto<Guid> CreatedBy { get; set; }
    public required DateTime UpdatedAt { get; init; }
    public required KeyValueDto<Guid> UpdatedBy { get; set; }
}