using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Uplansi.Core.Entities.Common;

namespace Uplansi.Core.Entities.Account;

public class ApplicationUser : IdentityUser<Guid>
{
    public string? FullName { get; set; }
    public required string DisplayName { get; set; }
    public string? Gender { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Many-to-One relationship with Language
    public required string LanguageId { get; set; }
    public Language? Language { get; set; }
    
    // Many-to-One relationship with Language
    public required string CountryId { get; set; }
    public Country? Country { get; set; }
    
    /* [ForeignKey("CreatedBy")]
    public required Guid CreatedById { get; set; }
    public ApplicationUser? CreatedBy { get; set; }
    
    [ForeignKey("UpdatedBy")]
    public required Guid UpdatedById { get; set; }
    public ApplicationUser? UpdatedBy { get; set; } */
    
}