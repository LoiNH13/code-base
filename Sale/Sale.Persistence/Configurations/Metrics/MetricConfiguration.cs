using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sale.Domain.Entities.Metrics;

namespace Sale.Persistence.Configurations.Metrics;

internal sealed class MetricConfiguration : IEntityTypeConfiguration<Metric>
{
    public void Configure(EntityTypeBuilder<Metric> builder)
    {
        builder.ToTable("metrics");

        builder.HasKey(metric => metric.Id);

        builder.Property(metric => metric.Id).HasColumnName("id");

        builder.Property(metric => metric.CustomerTimeFrameId).HasColumnName("customer_time_frame_id").IsRequired();

        builder.Property(metric => metric.ProductId).HasColumnName("product_id").IsRequired();

        builder.Property(metric => metric.OrderNumber).HasColumnName("order_number").IsRequired().HasDefaultValue(0);

        builder.Property(metric => metric.ReturnNumber).HasColumnName("order_value").IsRequired().HasDefaultValue(0);

        builder.Property(metric => metric.OrderIds).HasColumnName("order_ids")
            .HasConversion(v => string.Join(',', v), v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList(),
                new ValueComparer<List<int>>(
                    (c1, c2) => c1!.SequenceEqual(c2!),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()
                )
            );

        builder.Property(metric => metric.ReturnIds).HasColumnName("return_ids")
            .HasConversion(v => string.Join(',', v), v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList(),
                new ValueComparer<List<int>>(
                    (c1, c2) => c1!.SequenceEqual(c2!),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()
                )
            );

        builder.Property(metric => metric.CreatedOnUtc).HasColumnName("created_on_utc").IsRequired();

        builder.Property(metric => metric.ModifiedOnUtc).HasColumnName("modified_on_utc");

        builder.Property(metric => metric.DeletedOnUtc).HasColumnName("deleted_on_utc");

        builder.Property(metric => metric.Deleted).HasColumnName("deleted").IsRequired().HasDefaultValue(false);

        builder.HasOne(x => x.Product).WithMany().HasForeignKey(metric => metric.ProductId).IsRequired().OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.ForeCast).WithOne().HasForeignKey<ForeCast>(fc => fc.MetricId).OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Target).WithOne().HasForeignKey<Target>(tg => tg.MetricId).OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.OriginalBudget).WithOne().HasForeignKey<OriginalBudget>(ob => ob.MetricId).OnDelete(DeleteBehavior.NoAction);

        builder.HasIndex(metric => new { metric.CustomerTimeFrameId, metric.ProductId }).HasFilter("deleted = false").IsUnique();

        builder.HasQueryFilter(metric => !metric.Deleted);
    }
}
