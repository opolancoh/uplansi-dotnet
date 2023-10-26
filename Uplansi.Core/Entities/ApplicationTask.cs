using Uplansi.Core.Entities.Account;

namespace Uplansi.Core.Entities;

public class ApplicationTask : EntityBase
{
    public required string Title { get; init; }
    public string? Description { get; init; }
    public required int Priority { get; init; }
    public DateTime? DueDate { get; init; }
    public int? Progress { get; init; }
    public required bool Completed { get; init; }
    public TaskAcceptance Acceptance { get; init; }
    public required string GroupName { get; init; }

    // Foreign Keys
    public required Guid AssignedToId { get; set; }
    public ApplicationUser? AssignedTo { get; set; }

    public required Guid CreatedById { get; set; }
    public ApplicationUser? CreatedBy { get; set; }

    public required Guid UpdatedById { get; set; }
    public ApplicationUser? UpdatedBy { get; set; }
}

public enum TaskAcceptance
{
    NotAccepted = 0,
    Accepted,
    Rejected,
}