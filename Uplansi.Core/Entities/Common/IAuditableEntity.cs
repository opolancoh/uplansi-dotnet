using Uplansi.Core.Entities.Account;

namespace Uplansi.Core.Entities.Common;

public interface IAuditableEntity
{
    DateTime CreatedAt { get; set; }
    DateTime UpdatedAt { get; set; }

    Guid CreatedById { get; set; }
    ApplicationUser? CreatedBy { get; set; }

    Guid UpdatedById { get; set; }
    ApplicationUser? UpdatedBy { get; set; }
}
