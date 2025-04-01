using Domain.Core.Abstractions;
using Domain.Core.Primitives;

namespace Sms.Domain.Entities;

public sealed class ResMo(string servicePhone, double pricePerMo, double freeMtPerMo)
    : Entity(Guid.NewGuid()), IAuditableEntity, ISoftDeletableEntity
{
    public string ServicePhone { get; private set; } = servicePhone;

    public double PricePerMo { get; private set; } = pricePerMo;

    public double FreeMtPerMo { get; private set; } = freeMtPerMo;

    public DateTime CreatedOnUtc { get; }
    public DateTime? ModifiedOnUtc { get; }
    public DateTime? DeletedOnUtc { get; }
    public bool Deleted { get; }
}