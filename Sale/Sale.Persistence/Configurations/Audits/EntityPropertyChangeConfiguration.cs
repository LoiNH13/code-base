using Domain.Entities.Audits;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sale.Persistence.Configurations.Audits;

internal sealed class EntityPropertyChangeConfiguration : IEntityTypeConfiguration<EntityPropertyChange>
{
    public void Configure(EntityTypeBuilder<EntityPropertyChange> builder)
    {
        builder.ToTable("entity_property_changes");
        builder.HasKey(x => x.Id);
        builder.Property(propertyChange => propertyChange.Id).HasColumnName("id");

        builder.Property(propertyChange => propertyChange.EntityChangeId).HasColumnName("entity_change_id").IsRequired();
        builder.Property(propertyChange => propertyChange.PropertyName).HasColumnName("property_name").HasMaxLength(256).IsRequired();
        builder.Property(propertyChange => propertyChange.PropertyType).HasColumnName("property_type").HasMaxLength(256).IsRequired();
        builder.Property(propertyChange => propertyChange.OriginalValue).HasColumnType("jsonb").HasColumnName("original_value");
        builder.Property(propertyChange => propertyChange.NewValue).HasColumnType("jsonb").HasColumnName("new_value");
        builder.Property(propertyChange => propertyChange.CreatedOnUtc).HasColumnName("created_on_utc");

        builder.HasOne<EntityChange>()
            .WithMany(entityChange => entityChange.PropertyChanges)
            .HasForeignKey(propertyChange => propertyChange.EntityChangeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
