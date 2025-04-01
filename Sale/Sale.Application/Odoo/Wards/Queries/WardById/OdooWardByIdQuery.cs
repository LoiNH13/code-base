using Domain.Core.Primitives.Maybe;
using Sale.Contract.Odoo.Wards;

namespace Sale.Application.Odoo.Wards.Queries.WardById;

public sealed class OdooWardByIdQuery : IQuery<Maybe<OdooWardResponse>>
{
    public int Id { get; }

    public OdooWardByIdQuery(int id) => Id = id;
}
