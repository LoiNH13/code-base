using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sale.Domain.Entities.Users;

namespace Sale.Persistence.Configurations.Users;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(user => user.Id);
        builder.Property(user => user.Id).HasColumnName("id");

        builder.Property(user => user.Name).HasColumnName("name").IsRequired().HasMaxLength(256);

        builder.OwnsOne(user => user.Email, emailBuilder =>
        {
            emailBuilder.WithOwner();
            emailBuilder.Property(email => email.Value)
                .HasColumnName("email")
                .HasMaxLength(Email.MaxLength)
                .IsRequired();
            emailBuilder.HasIndex(email => email.Value);
        });

        builder.Property<string>("_passwordHash")
            .HasField("_passwordHash")
            .HasColumnName("password_hash")
            .IsRequired();

        builder.Property(user => user.ManagedByUserId).HasColumnName("managed_by_user_id");

        builder.Property(user => user.BusinessType).HasColumnName("business_type").IsRequired();

        builder.Property(user => user.Role).HasColumnName("role").IsRequired();

        builder.Property(user => user.CreatedOnUtc).HasColumnName("created_on_utc").IsRequired();

        builder.Property(user => user.ModifiedOnUtc).HasColumnName("modified_on_utc");

        builder.Property(user => user.DeletedOnUtc).HasColumnName("deleted_on_utc");

        builder.Property(user => user.Deleted).HasColumnName("deleted").HasDefaultValue(false);

        builder.HasOne(user => user.ManagedByUser).WithMany(user => user.SubordinateUsers).HasForeignKey(user => user.ManagedByUserId).OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(user => !user.Deleted);
    }
}
