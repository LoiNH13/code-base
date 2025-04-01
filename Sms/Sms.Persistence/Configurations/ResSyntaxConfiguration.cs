using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sms.Domain.Entities;

namespace Sms.Persistence.Configurations;

internal sealed class ResSyntaxConfiguration : IEntityTypeConfiguration<ResSyntax>
{
    public void Configure(EntityTypeBuilder<ResSyntax> builder)
    {
        builder.ToTable("res_syntaxes");

        builder.HasKey(rs => rs.Id);
        builder.Property(rs => rs.Id).HasColumnName("id");

        builder.Property(rs => rs.SyntaxName).HasColumnName("syntax_name").IsRequired();

        builder.Property(rs => rs.Description).HasColumnName("description");

        builder.Property(rs => rs.SyntaxValue).HasColumnName("syntax_value").IsRequired();

        builder.Property(rs => rs.SyntaxRegex).HasColumnName("syntax_regex");

        builder.Property(rs => rs.Metadata).HasColumnName("metadata");

        builder.Property(rs => rs.Inactive).HasColumnName("inactive").HasDefaultValue(false);

        builder.Property(rs => rs.CreatedOnUtc).HasColumnName("created_on_utc").IsRequired();

        builder.Property(rs => rs.ModifiedOnUtc).HasColumnName("modified_on_utc");

        builder.Property(rs => rs.DeletedOnUtc).HasColumnName("deleted_on_utc");

        builder.Property(rs => rs.Deleted).HasColumnName("deleted").HasDefaultValue(false);

        builder.HasIndex(rs => rs.SyntaxValue).IsUnique().HasFilter("deleted = false");

        builder.HasQueryFilter(mm => !mm.Deleted);
    }
}