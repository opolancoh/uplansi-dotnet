using System.ComponentModel.DataAnnotations.Schema;
using Uplansi.Core.Entities.Account;
using Uplansi.Core.Entities.Common;

namespace Uplansi.Core.Entities;

public class ApplicationTask : EntityBase// , IAuditableEntity
{
    public required string Title { get; init; }
    public string? Description { get; init; }
    public required TaskPriority Priority { get; init; }
    public DateTime? DueDate { get; init; }
    public int? Progress { get; init; }
    public required bool Completed { get; init; }
    public TaskAcceptance Acceptance { get; init; }
    public required string GroupName { get; init; }

    // Foreign Keys
    [ForeignKey("AssignedTo")]
    public Guid AssignedToId { get; set; }
    public ApplicationUser AssignedTo { get; set; }
    
    // IAuditableEntity implementation
    /* public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid CreatedById { get; set; }
    public ApplicationUser CreatedBy { get; set; }
    public Guid UpdatedById { get; set; }
    public ApplicationUser UpdatedBy { get; set; } */
}

public enum TaskAcceptance
{
    NotAccepted = 0,
    Accepted,
    Rejected,
}

public enum TaskPriority
{
    Low = 1,
    Normal,
    High,
}