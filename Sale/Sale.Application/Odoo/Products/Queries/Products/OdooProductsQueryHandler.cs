using Application.Core.Extensions;
using Contract.Common;
using Domain.Core.Primitives.Maybe;
using Microsoft.EntityFrameworkCore;
using Odoo.Domain.Entities;
using Odoo.Domain.Repositories;
using Sale.Contract.Odoo.Products;

namespace Sale.Application.Odoo.Products.Queries.Products;

internal sealed class OdooProductsQueryHandler(IOdooProductRepository odooProductRepository)
    : IQueryHandler<OdooProductsQuery, Maybe<PagedList<OdooProductResponse>>>
{
    public async Task<Maybe<PagedList<OdooProductResponse>>> Handle(OdooProductsQuery request,
        CancellationToken cancellationToken)
    {
        IQueryable<ProductProduct> query = odooProductRepository.Queryable(request.SearchText)
            .PaginateNotEntity(request.PageNumber, request.PageSize, out Paged paged);

        if (paged.NotExists()) return new PagedList<OdooProductResponse>(paged);

        List<OdooProductResponse> data =
            await query.Select(x => new OdooProductResponse(x)).ToListAsync(cancellationToken);
        return new PagedList<OdooProductResponse>(paged, data);
    }
}