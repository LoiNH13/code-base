using Application.Core.Extensions;
using Contract.Common;
using Domain.Core.Primitives.Maybe;
using Microsoft.EntityFrameworkCore;
using Odoo.Domain.Repositories;
using Sale.Contract.Odoo.Customers;

namespace Sale.Application.Odoo.Customers.Queries.Customers;

internal sealed class OdooCustomersQueryHandler(IOdooCustomerRepository odooCustomerRepository)
    : IQueryHandler<OdooCustomersQuery, Maybe<PagedList<OdooCustomerResponse>>>
{
    public async Task<Maybe<PagedList<OdooCustomerResponse>>> Handle(OdooCustomersQuery request,
        CancellationToken cancellationToken)
    {
        var query = odooCustomerRepository.Queryable(request.SearchText)
            .OrderByDescending(x => x.WriteDate)
            .PaginateNotEntity(request.PageNumber, request.PageSize, out Paged paged);

        if (paged.NotExists()) return new PagedList<OdooCustomerResponse>(paged);

        List<OdooCustomerResponse> data = await query.Select(x => new OdooCustomerResponse
        {
            Id = x.Id,
            Name = x.Name,
            InternalRef = x.Ref,
        }).ToListAsync(cancellationToken);

        return new PagedList<OdooCustomerResponse>(paged, data);
    }
}