using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uplansi.Core.Entities.Account;

namespace Uplansi.Data.EntityFramework.Configurations.Account;

public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public const string SystemRoleId = "42b42706-61fd-4e60-8129-d1570382a9bd";
    public const string AdminRoleId = "8dfff7f0-1bf5-4dd1-b217-cb7694ed789f";

    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        var roles = new List<ApplicationRole>()
        {
            new() { Id = Guid.Parse(SystemRoleId), Name = "System", NormalizedName = "SYSTEM" },
            new() { Id = Guid.Parse(AdminRoleId), Name = "Administrator", NormalizedName = "ADMINISTRATOR" }
        };
        builder.HasData(roles);
    }
}