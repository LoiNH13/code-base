using Application.Core.Extensions;
using Contract.Common;
using Domain.Core.Primitives.Maybe;
using Microsoft.EntityFrameworkCore;
using Odoo.Domain.Entities;
using Odoo.Domain.Repositories;
using Sale.Contract.Odoo.Wards;

namespace Sale.Application.Odoo.Wards.Queries.Wards;

internal sealed class OdooWardsQueryHandler(IOdooWardRepository odooWardRepository)
    : IQueryHandler<OdooWardsQuery, Maybe<PagedList<OdooWardResponse>>>
{
    public async Task<Maybe<PagedList<OdooWardResponse>>> Handle(OdooWardsQuery request,
        CancellationToken cancellationToken)
    {
        IQueryable<ResWard> query = odooWardRepository.Queryable(request.DistrictId, request.SearchText)
            .PaginateNotEntity(request.PageNumber, request.PageSize, out Paged paged);
        if (paged.NotExists()) return new PagedList<OdooWardResponse>(paged);

        List<OdooWardResponse> data = await query.Select(x => new OdooWardResponse(x)).ToListAsync();
        return new PagedList<OdooWardResponse>(paged, data);
    }
}