using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sale.Domain.Entities.Customers;
using Sale.Domain.Entities.Planning;
using Sale.Domain.Entities.Users;
using Sale.Domain.Enumerations;

namespace Sale.Persistence.Configurations.Planning;

internal sealed class PlanningApprovalConfiguration : IEntityTypeConfiguration<PlanningApproval>
{
    public void Configure(EntityTypeBuilder<PlanningApproval> builder)
    {
        builder.ToTable("planning_approvals");

        builder.HasKey(pa => pa.Id);
        builder.Property(pa => pa.Id).HasColumnName("id");

        builder.Property(pa => pa.PlanningControlId).HasColumnName("planning_control_id").IsRequired();

        builder.Property(pa => pa.CustomerId).HasColumnName("customer_id").IsRequired();

        builder.Property(pa => pa.CustomerManagedBy).HasColumnName("customer_managed_by");

        builder.Property(pa => pa.Status).HasColumnName("status")
            .HasConversion(v => v.Value, v => new PlanningApprovalStatus(v))
            .IsRequired().HasDefaultValue(PlanningApprovalStatus.WaitingForApproval);

        builder.Property(pa => pa.TotalOriginalBudgetAmount).HasColumnName("total_ob_amount");

        builder.Property(pa => pa.TotalTargetAmount).HasColumnName("total_target_amount");

        builder.Property(pa => pa.StatusByUserId).HasColumnName("status_by_user_id").IsRequired();

        builder.Property(pa => pa.CreatedOnUtc).HasColumnName("created_on_utc").IsRequired();

        builder.Property(pa => pa.ModifiedOnUtc).HasColumnName("modified_on_utc");

        builder.Property(pa => pa.DeletedOnUtc).HasColumnName("deleted_on_utc");

        builder.Property(pa => pa.Deleted).HasColumnName("deleted").HasDefaultValue(false).IsRequired();

        builder.HasOne<Customer>().WithMany().HasForeignKey(pa => pa.CustomerId).OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.PlanningControl).WithMany().HasForeignKey(pa => pa.PlanningControlId).OnDelete(DeleteBehavior.NoAction);

        builder.HasOne<User>().WithMany().HasForeignKey(pa => pa.StatusByUserId).OnDelete(DeleteBehavior.NoAction);

        builder.HasIndex(pa => new { pa.PlanningControlId, pa.CustomerId }).HasFilter("deleted = false").IsUnique();

        builder.HasQueryFilter(pc => !pc.Deleted);
    }
}
