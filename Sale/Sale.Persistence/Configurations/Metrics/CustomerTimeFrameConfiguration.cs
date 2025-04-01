using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sale.Domain.Entities.Metrics;

namespace Sale.Persistence.Configurations.Metrics;

internal sealed class CustomerTimeFrameConfiguration : IEntityTypeConfiguration<CustomerTimeFrame>
{
    public void Configure(EntityTypeBuilder<CustomerTimeFrame> builder)
    {
        builder.ToTable("customer_time_frames_rel");

        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).HasColumnName("id");

        builder.Property(t => t.CustomerId).HasColumnName("customer_id").IsRequired();

        builder.Property(t => t.TimeFrameId).HasColumnName("time_frame_id").IsRequired();

        builder.Property(t => t.CreatedOnUtc).HasColumnName("created_on_utc").IsRequired();

        builder.Property(t => t.ModifiedOnUtc).HasColumnName("modified_on_utc");

        builder.HasIndex(t => new { t.CustomerId, t.TimeFrameId }).IsUnique();

        builder.HasOne(x => x.TimeFrame).WithMany().HasForeignKey(t => t.TimeFrameId).OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Metrics).WithOne().HasForeignKey(m => m.CustomerTimeFrameId).OnDelete(DeleteBehavior.Restrict);
    }
}
