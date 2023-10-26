namespace Uplansi.Core.DTOs;

public record UserListResult
{
    public Guid Id { get; init; }
    public required string UserName { get; init; }
    public string? FullName { get; init; }
    public required string DisplayName { get; init; }
    public required string Country { get; init; }
    public required string Language { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required KeyValueDto<Guid> CreatedBy { get; set; }
    public required DateTime UpdatedAt { get; init; }
    public required KeyValueDto<Guid> UpdatedBy { get; set; }
}