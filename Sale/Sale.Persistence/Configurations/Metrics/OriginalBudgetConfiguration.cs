using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sale.Domain.Entities.Metrics;

namespace Sale.Persistence.Configurations.Metrics;

internal sealed class OriginalBudgetConfiguration : IEntityTypeConfiguration<OriginalBudget>
{
    public void Configure(EntityTypeBuilder<OriginalBudget> builder)
    {
        builder.ToTable("original_budgets");

        builder.HasKey(ob => ob.Id);
        builder.Property(ob => ob.Id).HasColumnName("id");

        builder.Property(ob => ob.MetricId).HasColumnName("metric_id").IsRequired();

        builder.Property(ob => ob.Price).HasColumnName("price").IsRequired();

        builder.Property(ob => ob.LastStockNumber).HasColumnName("last_stock_number").IsRequired().HasDefaultValue(0);

        builder.Property(ob => ob.WholeSalesNumber).HasColumnName("whole_sales_number").IsRequired().HasDefaultValue(0);

        builder.Property(ob => ob.RetailSalesNumber).HasColumnName("retail_sales_number").IsRequired().HasDefaultValue(0);

        builder.Property(ob => ob.StockNumber).HasColumnName("stock_number").IsRequired().HasDefaultValue(0);

        builder.Property(ob => ob.CreatedOnUtc).HasColumnName("created_on_utc").IsRequired();

        builder.Property(ob => ob.ModifiedOnUtc).HasColumnName("modified_on_utc");

        builder.HasIndex(ob => ob.MetricId).IsUnique();
    }
}
