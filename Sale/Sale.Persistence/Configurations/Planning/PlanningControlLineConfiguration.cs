using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sale.Domain.Entities.Planning;

namespace Sale.Persistence.Configurations.Planning;

internal sealed class PlanningControlLineConfiguration : IEntityTypeConfiguration<PlanningControlLine>
{
    public void Configure(EntityTypeBuilder<PlanningControlLine> builder)
    {
        builder.ToTable("planning_control_lines");

        builder.HasKey(pcl => pcl.Id);
        builder.Property(pcl => pcl.Id).HasColumnName("id");

        builder.Property(pcl => pcl.PlanningControlId).HasColumnName("planning_control_id").IsRequired();

        builder.Property(pcl => pcl.TimeFrameId).HasColumnName("time_frame_id").IsRequired();

        builder.Property(pcl => pcl.IsOriginalBudget).HasColumnName("is_original_budget").HasDefaultValue(false).IsRequired();

        builder.Property(pcl => pcl.IsTarget).HasColumnName("is_target").HasDefaultValue(false).IsRequired();

        builder.Property(pcl => pcl.CreatedOnUtc).HasColumnName("created_on_utc").IsRequired();

        builder.Property(pcl => pcl.ModifiedOnUtc).HasColumnName("modified_on_utc");

        builder.HasOne(pcl => pcl.TimeFrame).WithMany().HasForeignKey(pcl => pcl.TimeFrameId).OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(pcl => new { pcl.PlanningControlId, pcl.TimeFrameId }).IsUnique();
    }
}
