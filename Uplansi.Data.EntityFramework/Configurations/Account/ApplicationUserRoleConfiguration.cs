using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uplansi.Core.Entities.Account;

namespace Uplansi.Data.EntityFramework.Configurations.Account;

public class ApplicationUserRoleConfiguration : IEntityTypeConfiguration<ApplicationUserRole>
{
    private const string SystemUserId = ApplicationUserConfiguration.SystemUserId;
    private const string SystemRoleId = ApplicationRoleConfiguration.SystemRoleId;
    
    private const string AdminUserId = ApplicationUserConfiguration.AdminUserId;
    private const string AdminRoleId = ApplicationRoleConfiguration.AdminRoleId;

    public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
    {
        var roles = new List<ApplicationUserRole>()
        {
            new() { UserId = Guid.Parse(SystemUserId), RoleId =  Guid.Parse(SystemRoleId)},
            new() { UserId = Guid.Parse(AdminUserId), RoleId =  Guid.Parse(AdminRoleId)},
        };
        
        builder.HasData(roles);
    }
} 