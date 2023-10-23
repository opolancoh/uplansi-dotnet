using Uplansi.Core.Entities.Account;

namespace Uplansi.Core.Entities.Common;

public class Language
{
    // ISO 639-1
    public required string Id { get; init; }
    public required string Name { get; init; }
    
    public ICollection<ApplicationUser> ApplicationUsers { get; set; }
}