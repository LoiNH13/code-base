using Domain.Core.Primitives.Maybe;
using Odoo.Domain.Repositories;
using Sale.Contract.Odoo.Wards;

namespace Sale.Application.Odoo.Wards.Queries.WardById;

internal sealed class OdooWardByIdQueryHandler(IOdooWardRepository odooWardRepository)
    : IQueryHandler<OdooWardByIdQuery, Maybe<OdooWardResponse>>
{
    public async Task<Maybe<OdooWardResponse>> Handle(OdooWardByIdQuery request, CancellationToken cancellationToken) =>
        await odooWardRepository.GetByIdAsync(request.Id)
            .Match(x => new OdooWardResponse(x), () => default!);
}