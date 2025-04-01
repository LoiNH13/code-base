using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sale.Domain.Entities;

namespace Sale.Persistence.Configurations;

internal sealed class TimeFrameConfiguration : IEntityTypeConfiguration<TimeFrame>
{
    public void Configure(EntityTypeBuilder<TimeFrame> builder)
    {
        builder.ToTable("time_frames", t =>
        {
            t.HasCheckConstraint("CK_TimeFrame_Month", "month >= 1 AND month <= 12");
            t.HasCheckConstraint("CK_TimeFrame_Year", "(year)::numeric <= EXTRACT(year FROM CURRENT_DATE) + 1");
        });

        builder.HasKey(tf => tf.Id);
        builder.Property(tf => tf.Id).HasColumnName("id");

        builder.Property(tf => tf.Year).HasColumnName("year").IsRequired();

        builder.Property(tf => tf.Month).HasColumnName("month").IsRequired();

        builder.Property(tf => tf.CreatedOnUtc).HasColumnName("created_on_utc").IsRequired();

        builder.Property(tf => tf.ModifiedOnUtc).HasColumnName("modified_on_utc");

        builder.HasIndex(tf => new { tf.Year, tf.Month }).IsUnique();
    }
}
