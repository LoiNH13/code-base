using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sale.Domain.Entities.Customers;
using Sale.Domain.Entities.Users;

namespace Sale.Persistence.Configurations.Customers;

internal sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("customers");

        builder.HasKey(cus => cus.Id);
        builder.Property(cus => cus.Id).HasColumnName("id");

        builder.Property(cus => cus.Name).HasColumnName("name").IsRequired();

        builder.Property(cus => cus.OdooRef).HasColumnName("odoo_ref");

        builder.Property(user => user.BusinessType).HasColumnName("business_type").IsRequired();

        builder.Property(cus => cus.ManagedByUserId).HasColumnName("managed_by_user_id");

        builder.Property(cus => cus.VisitPerMonth).HasColumnName("visit_per_month");

        builder.Property(cus => cus.CreatedOnUtc).HasColumnName("created_on_utc").IsRequired();

        builder.Property(cus => cus.ModifiedOnUtc).HasColumnName("modified_on_utc");

        builder.Property(cus => cus.DeletedOnUtc).HasColumnName("deleted_on_utc");

        builder.Property(cus => cus.Deleted).HasColumnName("deleted").HasDefaultValue(false);

        builder.HasOne<User>().WithMany().HasForeignKey(cus => cus.ManagedByUserId).OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(cus => cus.PlanNewCustomer).WithOne(x => x.Customer).HasForeignKey<PlanNewCustomer>(pnc => pnc.CustomerId).OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(cus => cus.CustomerTimeFrames).WithOne().HasForeignKey(ctf => ctf.CustomerId).OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(cus => cus.MonthlyReports).WithOne().HasForeignKey(mr => mr.CustomerId).OnDelete(DeleteBehavior.NoAction);

        builder.HasIndex(cus => cus.OdooRef).IsUnique().HasFilter("deleted = false");

        builder.HasQueryFilter(cus => !cus.Deleted);
    }
}
