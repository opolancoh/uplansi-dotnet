using Uplansi.Core.Entities;

namespace Uplansi.Core.DTOs;

public record TaskAddOrUpdate
{
    public required string Title { get; init; }
    public string? Description { get; init; }
    public int Priority { get; init; }
    public DateTime? DueDate { get; init; }
    public int? Progress { get; init; }
    public required bool Completed { get; init; }
    public TaskAcceptance Acceptance { get; init; }
    public required string GroupName { get; init; }
    
    public required Guid AssignedToId { get; set; }
}
