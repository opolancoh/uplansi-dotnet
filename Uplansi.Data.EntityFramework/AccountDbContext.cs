using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Uplansi.Core.Entities;
using Uplansi.Core.Entities.Account;
using Uplansi.Core.Entities.Common;
using Uplansi.Data.EntityFramework.Configurations;
using Uplansi.Data.EntityFramework.Configurations.Account;

namespace Uplansi.Data.EntityFramework;

public class AccountDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    // private readonly string _connectionString;
    
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    public AccountDbContext(DbContextOptions<AccountDbContext> options)
        : base(options)
    {
        // _connectionString = connectionString;
    }
    
    /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
        optionsBuilder.EnableSensitiveDataLogging();
    } */

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new ApplicationRoleConfiguration());
        modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
        modelBuilder.ApplyConfiguration(new ApplicationUserRoleConfiguration());
        modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration()); 
        
        // Ignore the entities that are created on ApplicationDbContext
        modelBuilder.Ignore<Language>();
        modelBuilder.Ignore<Country>();
        modelBuilder.Ignore<ApplicationTask>();
    }
}