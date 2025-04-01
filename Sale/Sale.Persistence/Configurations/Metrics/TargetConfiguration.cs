using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sale.Domain.Entities.Metrics;

namespace Sale.Persistence.Configurations.Metrics;

internal sealed class TargetConfiguration : IEntityTypeConfiguration<Target>
{
    public void Configure(EntityTypeBuilder<Target> builder)
    {
        builder.ToTable("targets");

        builder.HasKey(tg => tg.Id);
        builder.Property(tg => tg.Id).HasColumnName("id");

        builder.Property(tg => tg.MetricId).HasColumnName("metric_id").IsRequired();

        builder.Property(tg => tg.Price).HasColumnName("price").IsRequired();

        builder.Property(tg => tg.LastStockNumber).HasColumnName("last_stock_number").IsRequired().HasDefaultValue(0);

        builder.Property(tg => tg.WholeSalesNumber).HasColumnName("whole_sales_number").IsRequired().HasDefaultValue(0);

        builder.Property(tg => tg.RetailSalesNumber).HasColumnName("retail_sales_number").IsRequired().HasDefaultValue(0);

        builder.Property(tg => tg.StockNumber).HasColumnName("stock_number").IsRequired().HasDefaultValue(0);

        builder.Property(tg => tg.CreatedOnUtc).HasColumnName("created_on_utc").IsRequired();

        builder.Property(tg => tg.ModifiedOnUtc).HasColumnName("modified_on_utc");

        builder.HasIndex(tg => tg.MetricId).IsUnique();
    }
}
