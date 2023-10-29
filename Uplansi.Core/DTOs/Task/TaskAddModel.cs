using Uplansi.Core.Entities;

namespace Uplansi.Core.DTOs.Task;

public record TaskAddModel
{
    public required string Title { get; init; }
    public string? Description { get; init; }
    public TaskPriority Priority { get; init; }
    public DateTime? DueDate { get; init; }
    public required string GroupName { get; init; }
    public required Guid AssignedToId { get; set; }
}
