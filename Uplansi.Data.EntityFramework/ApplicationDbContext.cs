using Microsoft.EntityFrameworkCore;
using Uplansi.Core.Entities.Account;
using Uplansi.Core.Entities.Common;
using Uplansi.Data.EntityFramework.Configurations;

namespace Uplansi.Data.EntityFramework;

public class ApplicationDbContext : DbContext
{
    // private readonly string _connectionString;

    public DbSet<Language> Languages { get; set; }
    public DbSet<Country> Countries { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
        optionsBuilder.EnableSensitiveDataLogging();
    } */

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new LanguageConfiguration());
        modelBuilder.ApplyConfiguration(new CountryConfiguration());

        // Ignore the entities that are created on ApplicationDbContext
        modelBuilder.Ignore<ApplicationUser>();
    }
}