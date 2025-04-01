using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sms.Domain.Entities;

namespace Sms.Persistence.Configurations;

internal sealed class ResMoConfiguration : IEntityTypeConfiguration<ResMo>
{
    public void Configure(EntityTypeBuilder<ResMo> builder)
    {
        builder.ToTable("res_mo");

        builder.HasKey(rm => rm.Id);
        builder.Property(rm => rm.Id).HasColumnName("id");

        builder.Property(rm => rm.ServicePhone).HasColumnName("service_phone").IsRequired();

        builder.Property(rm => rm.PricePerMo).HasColumnName("price_per_mo").IsRequired();

        builder.Property(rm => rm.FreeMtPerMo).HasColumnName("free_mt_per_mo").IsRequired();

        builder.Property(rm => rm.CreatedOnUtc).HasColumnName("created_on_utc").IsRequired();

        builder.Property(rm => rm.ModifiedOnUtc).HasColumnName("modified_on_utc");

        builder.Property(rm => rm.DeletedOnUtc).HasColumnName("deleted_on_utc");

        builder.Property(rm => rm.Deleted).HasColumnName("deleted").HasDefaultValue(false);

        builder.HasIndex(rm => rm.ServicePhone).IsUnique().HasFilter("deleted = false");

        builder.HasQueryFilter(mm => !mm.Deleted);
    }
}