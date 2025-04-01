using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sale.Domain.Entities.Metrics;

namespace Sale.Persistence.Configurations.Metrics;

internal sealed class ForeCastConfiguration : IEntityTypeConfiguration<ForeCast>
{
    public void Configure(EntityTypeBuilder<ForeCast> builder)
    {
        builder.ToTable("forecasts");

        builder.HasKey(fc => fc.Id);
        builder.Property(fc => fc.Id).HasColumnName("id");

        builder.Property(fc => fc.MetricId).HasColumnName("metric_id").IsRequired();

        builder.Property(fc => fc.Price).HasColumnName("price").IsRequired();

        builder.Property(fc => fc.LastStockNumber).HasColumnName("last_stock_number").IsRequired().HasDefaultValue(0);

        builder.Property(fc => fc.WholeSalesNumber).HasColumnName("whole_sales_number").IsRequired().HasDefaultValue(0);

        builder.Property(fc => fc.RetailSalesNumber).HasColumnName("retail_sales_number").IsRequired().HasDefaultValue(0);

        builder.Property(fc => fc.StockNumber).HasColumnName("stock_number").IsRequired().HasDefaultValue(0);

        builder.Property(fc => fc.CreatedOnUtc).HasColumnName("created_on_utc").IsRequired();

        builder.Property(fc => fc.ModifiedOnUtc).HasColumnName("modified_on_utc");

        builder.HasIndex(fc => fc.MetricId).IsUnique();
    }
}
