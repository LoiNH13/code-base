using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sale.Domain.Entities.Products;

namespace Sale.Persistence.Configurations.Products;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");

        builder.HasKey(product => product.Id);
        builder.Property(product => product.Id).HasColumnName("id");

        builder.Property(product => product.CategoryId).HasColumnName("category_id");

        builder.Property(product => product.Name).HasColumnName("name").IsRequired();

        builder.Property(product => product.OdooRef).HasColumnName("odoo_ref").IsRequired();

        builder.Property(product => product.OdooCode).HasColumnName("odoo_code");

        builder.Property(product => product.Price).HasColumnName("price").IsRequired().HasDefaultValue(0);

        builder.Property(product => product.Weight).HasColumnName("weight").IsRequired().HasDefaultValue(0);

        builder.Property(product => product.Inactive).HasColumnName("inactive").HasDefaultValue(false).IsRequired();

        builder.Property(product => product.CreatedOnUtc).HasColumnName("created_on_utc").IsRequired();

        builder.Property(product => product.ModifiedOnUtc).HasColumnName("modified_on_utc");

        builder.HasOne(product => product.Category).WithMany().HasForeignKey(product => product.CategoryId).OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(product => product.ProductTimeFramePrices).WithOne(price => price.Product).HasForeignKey(product => product.ProductId).OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(product => product.OdooRef).IsUnique();
    }
}
