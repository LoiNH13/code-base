using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sale.Domain.Entities.Planning;
using Sale.Domain.Enumerations;

namespace Sale.Persistence.Configurations.Planning;

internal sealed class PlanningControlConfiguration : IEntityTypeConfiguration<PlanningControl>
{
    public void Configure(EntityTypeBuilder<PlanningControl> builder)
    {
        builder.ToTable("planning_controls");

        builder.HasKey(pc => pc.Id);
        builder.Property(pc => pc.Id).HasColumnName("id");

        builder.Property(pc => pc.Name).HasColumnName("name").IsRequired();

        builder.Property(pc => pc.Status).HasColumnName("status")
            .HasConversion(v => v.Value, v => new PlanningControlStatus(v))
            .IsRequired().HasDefaultValue(PlanningControlStatus.New);

        builder.Property(pc => pc.CreatedOnUtc).HasColumnName("created_on_utc").IsRequired();

        builder.Property(pc => pc.ModifiedOnUtc).HasColumnName("modified_on_utc");

        builder.Property(pc => pc.DeletedOnUtc).HasColumnName("deleted_on_utc");

        builder.Property(pc => pc.Deleted).HasColumnName("deleted").HasDefaultValue(false).IsRequired();

        builder.HasMany(pc => pc.PlanningControlLines).WithOne().HasForeignKey(pc => pc.PlanningControlId).OnDelete(DeleteBehavior.NoAction);

        builder.HasQueryFilter(pc => !pc.Deleted);
    }
}
