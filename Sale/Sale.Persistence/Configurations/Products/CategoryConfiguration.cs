using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sale.Domain.Entities.Products;

namespace Sale.Persistence.Configurations.Products;

internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("categories");

        builder.HasKey(cate => cate.Id);
        builder.Property(cate => cate.Id).HasColumnName("id");

        builder.Property(cate => cate.OdooRef).HasColumnName("odoo_ref");

        builder.Property(cate => cate.Name).HasColumnName("name").IsRequired();

        builder.Property(cate => cate.Weight).HasColumnName("weight").IsRequired().HasDefaultValue(0);

        builder.Property(cate => cate.IsShowSalePlan).HasColumnName("is_show_sale_plan").HasDefaultValue(false).IsRequired();

        builder.Property(cate => cate.IsShowMonthlyReport).HasColumnName("is_show_monthly_report").HasDefaultValue(false).IsRequired();

        builder.Property(cate => cate.CreatedOnUtc).HasColumnName("created_on_utc").IsRequired();

        builder.Property(cate => cate.ModifiedOnUtc).HasColumnName("modified_on_utc");

        builder.Property(cate => cate.DeletedOnUtc).HasColumnName("deleted_on_utc");

        builder.Property(cate => cate.Deleted).HasColumnName("deleted").HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(cate => !cate.Deleted);
    }
}
