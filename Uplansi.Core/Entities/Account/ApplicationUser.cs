using Microsoft.AspNetCore.Identity;
using Uplansi.Core.Entities.Common;

namespace Uplansi.Core.Entities.Account;

public class ApplicationUser : IdentityUser<Guid>, IAuditableEntity
{
    public string? FullName { get; set; }
    public string DisplayName { get; set; }
    public string? Gender { get; set; }

    // Many-to-One relationship with Language
    public string LanguageId { get; set; }
    public Language? Language { get; set; }
    
    // Many-to-One relationship with Language
    public string CountryId { get; set; }
    public Country? Country { get; set; }
    
    // IAuditableEntity implementation
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid CreatedById { get; set; }
    public ApplicationUser CreatedBy { get; set; }
    public Guid UpdatedById { get; set; }
    public ApplicationUser UpdatedBy { get; set; }
    
    // 
    public ICollection<ApplicationTask> AssignedToTasks { get; set; }
    // public ICollection<ApplicationTask> CreatedByTasks { get; set; }
    // public ICollection<ApplicationTask> UpdatedByTasks { get; set; }
}