using Domain.Core.Primitives.Maybe;
using Sale.Contract.Odoo.States;

namespace Sale.Application.Odoo.States.Queries.StateById;

public sealed class OdooStateByIdQuery : IQuery<Maybe<OdooStateResponse>>
{
    public int Id { get; }

    public OdooStateByIdQuery(int id) => Id = id;
}
