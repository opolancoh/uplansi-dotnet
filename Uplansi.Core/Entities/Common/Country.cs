using Uplansi.Core.Entities.Account;

namespace Uplansi.Core.Entities.Common;

public class Country
{
    // ISO 3166-1 - Alpha-2 code
    public required string Id { get; init; }
    public required string Name { get; init; }
    
    public ICollection<ApplicationUser> ApplicationUsers { get; set; }
}