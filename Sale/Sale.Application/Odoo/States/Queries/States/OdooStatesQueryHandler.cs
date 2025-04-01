using Application.Core.Extensions;
using Contract.Common;
using Domain.Core.Primitives.Maybe;
using Microsoft.EntityFrameworkCore;
using Odoo.Domain.Entities;
using Odoo.Domain.Repositories;
using Sale.Contract.Odoo.States;

namespace Sale.Application.Odoo.States.Queries.States;

internal sealed class OdooStatesQueryHandler(IOdooStateRepository odooStateRepository)
    : IQueryHandler<OdooStatesQuery, Maybe<PagedList<OdooStateResponse>>>
{
    public async Task<Maybe<PagedList<OdooStateResponse>>> Handle(OdooStatesQuery request,
        CancellationToken cancellationToken)
    {
        IQueryable<ResCountryState> query = odooStateRepository.Queryable(request.SearchText)
            .PaginateNotEntity(request.PageNumber, request.PageSize, out Paged paged);
        if (paged.NotExists()) return new PagedList<OdooStateResponse>(paged);

        List<OdooStateResponse> data = await query.Select(s => new OdooStateResponse(s)).ToListAsync();
        return new PagedList<OdooStateResponse>(paged, data);
    }
}