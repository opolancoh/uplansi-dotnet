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
            .HasOne(at => at.AssignedTo)
            .WithMany(au => au.AssignedToTasks)
            .HasForeignKey(at => at.AssignedToId)
            .OnDelete(DeleteBehavior.Restrict); 
        
        /* builder
           .HasOne(at => at.CreatedBy)
           .WithMany(au => au.CreatedByTasks)
           .HasForeignKey(at => at.CreatedById)
           .OnDelete(DeleteBehavior.Restrict);
           
        builder
           .HasOne(at => at.UpdatedBy)
           .WithMany(au => au.UpdatedByTasks)
           .HasForeignKey(at => at.UpdatedById)
           .OnDelete(DeleteBehavior.Restrict); */
    }
}