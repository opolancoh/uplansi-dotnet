using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uplansi.Core.Entities.Common;

namespace Uplansi.Data.EntityFramework.Configurations;

public class LanguageConfiguration : IEntityTypeConfiguration<Language>
{
    public void Configure(EntityTypeBuilder<Language> builder)
    {
        builder.ToTable("Languages");

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
                new Language { Id = "en", Name = "English" },
                new Language { Id = "es", Name = "Spanish" }
            );
    }
}