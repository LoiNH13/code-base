using Domain.Core.Primitives.Maybe;
using Odoo.Domain.Repositories;
using Sale.Contract.Odoo.Districts;

namespace Sale.Application.Odoo.Districts.Queries.DistrictById;

internal sealed class OdooDistrictByIdQueryHandler(IOdooDistrictRepository districtRepository)
    : IQueryHandler<OdooDistrictByIdQuery, Maybe<OdooDistrictResponse>>
{
    public async Task<Maybe<OdooDistrictResponse>> Handle(OdooDistrictByIdQuery request,
        CancellationToken cancellationToken) =>
        await districtRepository.GetByIdAsync(request.Id)
            .Match(x => new OdooDistrictResponse(x), () => default!);
}