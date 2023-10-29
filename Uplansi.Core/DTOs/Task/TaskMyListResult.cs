using Uplansi.Core.Entities;

namespace Uplansi.Core.DTOs.Task;

public record TaskMyListResult
{
    public Guid Id { get; init; }
    public required string Title { get; init; }
    public string? Description { get; init; }
    public required TaskPriority Priority { get; init; }
    public DateTime? DueDate { get; init; }
    public int? Progress { get; init; }
    public required bool Completed { get; init; }
}