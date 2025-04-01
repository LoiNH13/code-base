using Domain.Core.Primitives.Maybe;
using Sale.Contract.Odoo.Districts;

namespace Sale.Application.Odoo.Districts.Queries.DistrictById;

public sealed class OdooDistrictByIdQuery : IQuery<Maybe<OdooDistrictResponse>>
{
    public int Id { get; }

    public OdooDistrictByIdQuery(int id) => Id = id;
}
