using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uplansi.Core.Entities.Account;

namespace Uplansi.Data.EntityFramework.Configurations.Account;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public const string SystemUserId = "42b42706-61fd-4e60-8129-d1570382a9bd";
    public const string AdminUserId = "8dfff7f0-1bf5-4dd1-b217-cb7694ed789f";

    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder
            .Property(e => e.FullName)
            .HasMaxLength(80);

        builder
            .Property(e => e.DisplayName)
            .IsRequired()
            .HasMaxLength(15);

        builder
            .Property(e => e.Gender)
            .HasMaxLength(1);
        
        // Configure Many-to-One
        builder
            .HasOne(x => x.Language)
            .WithMany(x=>x.ApplicationUsers)
            .HasForeignKey(x => x.LanguageId);
        
        // Configure Many-to-One
        builder
            .HasOne(x => x.Country)
            .WithMany(x=>x.ApplicationUsers)
            .HasForeignKey(x => x.CountryId);
        
        // Configure the self-referencing relationship for CreatedBy
        builder
            .HasOne(x => x.CreatedBy)
            .WithMany()
            .HasForeignKey(x => x.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);
        
        // Configure the self-referencing relationship for UpdatedBy
        builder
            .HasOne(x => x.UpdatedBy)
            .WithMany()
            .HasForeignKey(x => x.UpdatedById)
            .OnDelete(DeleteBehavior.Restrict);

        var users = new List<ApplicationUser>()
        {
            new()
            {
                Id = Guid.Parse(SystemUserId),
                FullName = "System",
                UserName = "system",
                DisplayName = "system",
                NormalizedUserName = "SYSTEM",
                Email = "system@ikobit.com",
                NormalizedEmail = "SYSTEM@IKOBIT.COM",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                LanguageId = "en",
                CountryId = "us",
                CreatedById = Guid.Parse(SystemUserId),
                UpdatedById = Guid.Parse(SystemUserId)
            },
            new()
            {
                Id = Guid.Parse(AdminUserId),
                FullName = "Admin",
                UserName = "admin",
                DisplayName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@ikobit.com",
                NormalizedEmail = "ADMIN@IKOBIT.COM",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                LanguageId = "en",
                CountryId = "us",
                CreatedById = Guid.Parse(SystemUserId),
                UpdatedById = Guid.Parse(SystemUserId)
            },
        };
        // Passwords
        var passwordHasher = new PasswordHasher<ApplicationUser>();

        var systemUser = users.Single(x => x.UserName == "system");
        systemUser.PasswordHash = passwordHasher.HashPassword(systemUser,
            Environment.GetEnvironmentVariable($"UPLANSI_API_USER_SYSTEM_PASSWORD")!);

        var adminUser = users.Single(x => x.UserName == "admin");
        adminUser.PasswordHash = passwordHasher.HashPassword(adminUser,
            Environment.GetEnvironmentVariable($"UPLANSI_API_USER_ADMIN_PASSWORD")!);

        builder.HasData(users);
    }
} 