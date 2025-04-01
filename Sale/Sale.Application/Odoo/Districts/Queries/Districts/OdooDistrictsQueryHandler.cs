using Application.Core.Extensions;
using Contract.Common;
using Domain.Core.Primitives.Maybe;
using Microsoft.EntityFrameworkCore;
using Odoo.Domain.Entities;
using Odoo.Domain.Repositories;
using Sale.Contract.Odoo.Districts;

namespace Sale.Application.Odoo.Districts.Queries.Districts;

internal sealed class OdooDistrictsQueryHandler(IOdooDistrictRepository odooDistrictRepository)
    : IQueryHandler<OdooDistrictsQuery, Maybe<PagedList<OdooDistrictResponse>>>
{
    public async Task<Maybe<PagedList<OdooDistrictResponse>>> Handle(OdooDistrictsQuery request,
        CancellationToken cancellationToken)
    {
        IQueryable<ResDistrict> query = odooDistrictRepository.Queryable(request.StateId, request.SearchText)
            .PaginateNotEntity(request.PageNumber, request.PageSize, out Paged paged);
        if (paged.NotExists()) return new PagedList<OdooDistrictResponse>(paged);

        List<OdooDistrictResponse> data = await query.Select(x => new OdooDistrictResponse(x)).ToListAsync();
        return new PagedList<OdooDistrictResponse>(paged, data);
    }
}