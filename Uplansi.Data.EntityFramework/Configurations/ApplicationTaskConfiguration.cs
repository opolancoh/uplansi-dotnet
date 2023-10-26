using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uplansi.Core.Entities;
using Uplansi.Core.Entities.Common;

namespace Uplansi.Data.EntityFramework.Configurations;

public class ApplicationTaskConfiguration : IEntityTypeConfiguration<ApplicationTask>
{
    public void Configure(EntityTypeBuilder<ApplicationTask> builder)
    {
        builder.ToTable("ApplicationTasks");

        builder
            .Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(80);
        
        builder
            .Property(e => e.Description)
            .HasMaxLength(4000);
        
        builder
            .Property(e => e.GroupName)
            .IsRequired()
            .HasMaxLength(50);
        
        builder
            .HasOne(u => u.AssignedTo)
            .WithMany()
            .HasForeignKey(u => u.AssignedToId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(u => u.CreatedBy)
            .WithMany()
            .HasForeignKey(u => u.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasOne(u => u.UpdatedBy)
            .WithMany()
            .HasForeignKey(u => u.UpdatedById)
            .OnDelete(DeleteBehavior.Restrict);
        
    }
}