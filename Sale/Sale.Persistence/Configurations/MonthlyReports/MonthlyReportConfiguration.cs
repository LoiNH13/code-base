using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sale.Domain.Entities.MonthlyReports;
using Sale.Domain.Entities.Users;

namespace Sale.Persistence.Configurations.MonthlyReports;

internal sealed class MonthlyReportConfiguration : IEntityTypeConfiguration<MonthlyReport>
{
    public void Configure(EntityTypeBuilder<MonthlyReport> builder)
    {
        builder.ToTable("monthly_reports");

        builder.HasKey(mr => mr.Id);
        builder.Property(mr => mr.Id).HasColumnName("id");

        builder.Property<string>("_dynamicData")
            .HasField("_dynamicData")
            .HasColumnName("dynamic_data")
            .HasColumnType("jsonb")
            .IsRequired();

        builder.Property(mr => mr.CustomerId).HasColumnName("customer_id").IsRequired();

        builder.Property(mr => mr.FromTimeOnUtc).HasColumnName("from_time_on_utc").IsRequired();

        builder.Property(mr => mr.ToTimeOnUtc).HasColumnName("to_time_on_utc").IsRequired();

        builder.Property(mr => mr.DailyVisitors).HasColumnName("daily_visitors").IsRequired();

        builder.Property(mr => mr.DailyPurchases).HasColumnName("daily_purchases").IsRequired();

        builder.Property(mr => mr.OnlinePurchaseRate).HasColumnName("online_purchase_rate").IsRequired();

        builder.Property(mr => mr.BusinessType).HasColumnName("business_type").IsRequired();

        builder.Property(mr => mr.Note).HasColumnName("note");

        builder.Property(mr => mr.IsConfirmed).HasColumnName("is_confirmed").HasDefaultValue(false);

        builder.Property(mr => mr.CreatedOnUtc).HasColumnName("created_on_utc").IsRequired();

        builder.Property(mr => mr.CreateByUser).HasColumnName("create_by_user").IsRequired();

        builder.Property(mr => mr.ModifiedOnUtc).HasColumnName("modified_on_utc");

        builder.Property(mr => mr.DeletedOnUtc).HasColumnName("deleted_on_utc");

        builder.Property(mr => mr.Deleted).HasColumnName("deleted").HasDefaultValue(false);

        builder.HasOne<User>().WithMany().HasForeignKey(mr => mr.CreateByUser).OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(mr => mr.Items).WithOne().HasForeignKey(item => item.MonthlyReportId).OnDelete(DeleteBehavior.NoAction);

        builder.HasQueryFilter(mr => !mr.Deleted);
    }
}
