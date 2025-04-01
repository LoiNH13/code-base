using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sale.Domain.Entities.Customers;

namespace Sale.Persistence.Configurations.Customers;

internal sealed class PlanNewCustomerConfiguration : IEntityTypeConfiguration<PlanNewCustomer>
{
    public void Configure(EntityTypeBuilder<PlanNewCustomer> builder)
    {
        builder.ToTable("plan_new_customers");

        builder.HasKey(pnc => pnc.Id);
        builder.Property(pnc => pnc.Id).HasColumnName("id");

        builder.Property(pnc => pnc.CustomerId).HasColumnName("customer_id").IsRequired();

        builder.Property(pnc => pnc.Code).HasColumnName("code").IsRequired();

        builder.Property(pnc => pnc.CityId).HasColumnName("city_id").IsRequired();

        builder.Property(pnc => pnc.DistrictId).HasColumnName("district_id");

        builder.Property(pnc => pnc.WardId).HasColumnName("ward_id");

        builder.Property(pnc => pnc.IsOpen).HasColumnName("is_open").HasDefaultValue(false).IsRequired();

        builder.Property(pnc => pnc.CreatedOnUtc).HasColumnName("created_on_utc").IsRequired();

        builder.Property(pnc => pnc.ModifiedOnUtc).HasColumnName("modified_on_utc");

        builder.Property(pnc => pnc.DeletedOnUtc).HasColumnName("deleted_on_utc");

        builder.Property(pnc => pnc.Deleted).HasColumnName("deleted").HasDefaultValue(false).IsRequired();

        builder.HasOne(x => x.Customer).WithOne(x => x.PlanNewCustomer).HasForeignKey<PlanNewCustomer>(pnc => pnc.CustomerId).OnDelete(DeleteBehavior.NoAction);

        builder.HasIndex(pnc => pnc.CustomerId).IsUnique().HasFilter("deleted = false");

        builder.HasIndex(pnc => pnc.Code).IsUnique();

        builder.HasQueryFilter(cus => !cus.Deleted);
    }
}
