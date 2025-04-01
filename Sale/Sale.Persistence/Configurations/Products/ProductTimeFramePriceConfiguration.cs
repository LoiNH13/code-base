using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sale.Domain.Entities.Products;

namespace Sale.Persistence.Configurations.Products;

internal sealed class ProductTimeFramePriceConfiguration : IEntityTypeConfiguration<ProductTimeFramePrice>
{
    public void Configure(EntityTypeBuilder<ProductTimeFramePrice> builder)
    {
        builder.ToTable("product_time_frame_prices");

        builder.HasKey(tfprice => tfprice.Id);
        builder.Property(tfprice => tfprice.Id).HasColumnName("id");

        builder.Property(product => product.ProductId).IsRequired().HasColumnName("tfprice_id").IsRequired();

        builder.Property(tfprice => tfprice.TimeFrameId).HasColumnName("time_frame_id").IsRequired();

        builder.Property(tfprice => tfprice.Price).HasColumnName("price").IsRequired();

        builder.Property(tfprice => tfprice.CreatedOnUtc).HasColumnName("created_on_utc");

        builder.Property(tfprice => tfprice.ModifiedOnUtc).HasColumnName("modified_on_utc");

        builder.HasOne(tfprice => tfprice.TimeFrame).WithMany().HasForeignKey(tfprice => tfprice.TimeFrameId).OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(tfprice => tfprice.Product).WithMany(product => product.ProductTimeFramePrices).HasForeignKey(tfprice => tfprice.ProductId).OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(tfprice => new { tfprice.ProductId, tfprice.TimeFrameId }).IsUnique();
    }
}
