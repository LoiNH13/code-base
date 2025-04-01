using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sale.Domain.Entities.MonthlyReports;

namespace Sale.Persistence.Configurations.MonthlyReports;

internal sealed class MonthlyReportItemConfiguration : IEntityTypeConfiguration<MonthlyReportItem>
{
    public void Configure(EntityTypeBuilder<MonthlyReportItem> builder)
    {
        builder.ToTable("monthly_report_items");

        builder.HasKey(mri => mri.Id);
        builder.Property(mri => mri.Id).HasColumnName("id");

        builder.Property(mri => mri.MonthlyReportId).HasColumnName("monthly_report_id").IsRequired();

        builder.Property(mri => mri.CategoryId).HasColumnName("category_id").IsRequired();

        builder.Property(mri => mri.Group).HasColumnName("group").IsRequired();

        builder.Property(mri => mri.Quantity).HasColumnName("quantity").IsRequired();

        builder.Property(mri => mri.Revenue).HasColumnName("revenue").IsRequired();

        builder.Property(mri => mri.Note).HasColumnName("note");

        builder.Property(mri => mri.CreatedOnUtc).HasColumnName("created_on_utc").IsRequired();

        builder.Property(mri => mri.ModifiedOnUtc).HasColumnName("modified_on_utc");

        builder.HasIndex(mri => new { mri.MonthlyReportId, mri.Group, mri.CategoryId }).IsUnique();
    }
}
