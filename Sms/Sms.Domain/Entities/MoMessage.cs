using Domain.Core.Abstractions;
using Domain.Core.Primitives;

namespace Sms.Domain.Entities;

public sealed class MoMessage(
    int moId,
    string telco,
    string serviceNum,
    string phone,
    string content,
    string encryptedMessage,
    string signature,
    string? metadata,
    string? partnerResponse,
    string moSource,
    Guid? resMoId)
    : AggregateRoot(Guid.NewGuid()), IAuditableEntity, ISoftDeletableEntity
{
    public int MoId { get; private set; } = moId;

    public string Telco { get; private set; } = telco;

    public string ServiceNum { get; private set; } = serviceNum;

    public string Phone { get; private set; } = phone;

    public string Content { get; private set; } = content;

    public string EncryptedMessage { get; private set; } = encryptedMessage;

    public string Signature { get; private set; } = signature;

    public string MoSource { get; private set; } = moSource;

    public string? Metadata { get; private set; } = metadata;

    public string? PartnerResponse { get; private set; } = partnerResponse;

    public Guid? ResMoId { get; private set; } = resMoId;

    public ResMo? ResMo { get; }

    public DateTime CreatedOnUtc { get; }

    public DateTime? ModifiedOnUtc { get; }

    public DateTime? DeletedOnUtc { get; }

    public bool Deleted { get; }
}