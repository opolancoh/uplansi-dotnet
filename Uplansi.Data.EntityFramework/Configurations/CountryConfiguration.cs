using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uplansi.Core.Entities.Common;

namespace Uplansi.Data.EntityFramework.Configurations;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ToTable("Countries");

        builder
            .HasKey(e => e.Id);

        builder
            .Property(e => e.Id)
            .IsRequired()
            .HasMaxLength(3);

        builder
            .Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .HasData(
                new Country { Id = "us", Name = "United States" },
                new Country { Id = "co", Name = "Colombia" }
            );
    }
}