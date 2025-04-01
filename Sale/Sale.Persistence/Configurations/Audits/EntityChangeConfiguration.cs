using Domain.Entities.Audits;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sale.Persistence.Configurations.Audits;

internal sealed class EntityChangeConfiguration : IEntityTypeConfiguration<EntityChange>
{
    public void Configure(EntityTypeBuilder<EntityChange> builder)
    {
        builder.ToTable("entity_changes");
        builder.HasKey(x => x.Id);
        builder.Property(change => change.Id).HasColumnName("id");

        builder.Property(change => change.ChangeType).HasColumnName("change_type").HasMaxLength(256).IsRequired();
        builder.Property(change => change.EntityId).HasColumnName("entity_id").IsRequired();
        builder.Property(change => change.EntityName).HasColumnName("entity_name").HasMaxLength(256).IsRequired();
        builder.Property(change => change.Route).HasColumnName("route");
        builder.Property(change => change.ClientIpAddress).HasColumnName("client_ip_address").HasMaxLength(256);
        builder.Property(change => change.RequestId).HasColumnName("request_id").HasMaxLength(256);
        builder.Property(change => change.CreateBy).HasColumnName("create_by");
        builder.Property(change => change.CreatedOnUtc).HasColumnName("created_on_utc").IsRequired();

        builder.HasMany(change => change.PropertyChanges)
            .WithOne()
            .HasForeignKey(propertyChange => propertyChange.EntityChangeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(change => change.EntityId);
    }
}
