using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sms.Domain.Entities;

namespace Sms.Persistence.Configurations;

public class MoMessageConfiguration : IEntityTypeConfiguration<MoMessage>
{
    public void Configure(EntityTypeBuilder<MoMessage> builder)
    {
        builder.ToTable("mo_messages");

        builder.HasKey(mm => mm.Id);
        builder.Property(mm => mm.Id).HasColumnName("id");

        builder.Property(mm => mm.MoId).HasColumnName("mo_id").IsRequired();

        builder.Property(mm => mm.Telco).HasColumnName("telco").IsRequired();

        builder.Property(mm => mm.ServiceNum).HasColumnName("service_num").IsRequired();

        builder.Property(mm => mm.Phone).HasColumnName("phone").IsRequired();

        builder.Property(mm => mm.Content).HasColumnName("content").IsRequired();

        builder.Property(mm => mm.EncryptedMessage).HasColumnName("encrypted_message").IsRequired();

        builder.Property(mm => mm.Signature).HasColumnName("signature").IsRequired();

        builder.Property(mm => mm.Metadata).HasColumnName("metadata");

        builder.Property(mm => mm.PartnerResponse).HasColumnName("partner_response");

        builder.Property(mm => mm.MoSource).HasColumnName("mo_source").IsRequired();

        builder.Property(mm => mm.ResMoId).HasColumnName("res_mo_id");

        builder.Property(mm => mm.CreatedOnUtc).HasColumnName("created_on_utc").IsRequired();

        builder.Property(mm => mm.ModifiedOnUtc).HasColumnName("modified_on_utc");

        builder.Property(mm => mm.DeletedOnUtc).HasColumnName("deleted_on_utc");

        builder.Property(mm => mm.Deleted).HasColumnName("deleted").HasDefaultValue(false);

        builder.HasOne(mm => mm.ResMo).WithMany().HasForeignKey(mm => mm.ResMoId);

        builder.HasQueryFilter(mm => !mm.Deleted);
    }
}