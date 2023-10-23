using System.ComponentModel.DataAnnotations.Schema;
using Uplansi.Core.Entities.Account;

namespace Uplansi.Core.Entities;

public class ApplicationTask
{
    public Guid Id { get; set; }
    public required string Title { get; init; }
    public string? Description { get; init; }
    public int Priority { get; init; }
    public DateTime DueDate { get; init; }
    public int Progress { get; init; }
    public bool Completed { get; init; }
    public int Acceptance { get; init; }
    public required string GroupName { get; init; }
    
    // Foreign Keys
    [ForeignKey(nameof(ApplicationUser))]
    public required Guid AssignedToId { get; set; }
    public ApplicationUser? AssignedTo { get; set; }
    
    [ForeignKey(nameof(ApplicationUser))]
    public required Guid CreatedById { get; set; }
    public ApplicationUser? CreatedBy { get; set; }
    
    [ForeignKey(nameof(ApplicationUser))]
    public required Guid UpdatedById { get; set; }
    public ApplicationUser? UpdatedBy { get; set; }
}