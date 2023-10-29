using Uplansi.Core.Entities;

namespace Uplansi.Core.DTOs.Task;

public record TaskMyDetailResult
{
    public Guid Id { get; init; }
    public required string Title { get; init; }
    public string? Description { get; init; }
    public required TaskPriority Priority { get; init; }
    public DateTime? DueDate { get; init; }
    public int? Progress { get; init; }
    public required bool Completed { get; init; }
    
    /* public required DateTime CreatedAt { get; init; }
    public required KeyValueDto<Guid> CreatedBy { get; set; }
    public required DateTime UpdatedAt { get; init; }
    public required KeyValueDto<Guid> UpdatedBy { get; set; } */
}