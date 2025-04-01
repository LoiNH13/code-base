using Domain.Core.Primitives.Maybe;
using Odoo.Domain.Repositories;
using Sale.Contract.Odoo.States;

namespace Sale.Application.Odoo.States.Queries.StateById;

internal sealed class OdooStateByIdQueryHandler(IOdooStateRepository odooStateRepository)
    : IQueryHandler<OdooStateByIdQuery, Maybe<OdooStateResponse>>
{
    public async Task<Maybe<OdooStateResponse>>
        Handle(OdooStateByIdQuery request, CancellationToken cancellationToken) =>
        await odooStateRepository.GetByIdAsync(request.Id)
            .Match(x => new OdooStateResponse(x), () => default!);
}