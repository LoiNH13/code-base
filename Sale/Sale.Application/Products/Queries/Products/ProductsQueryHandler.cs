using Application.Core.Extensions;
using Contract.Common;
using Domain.Core.Primitives.Maybe;
using Microsoft.EntityFrameworkCore;
using Sale.Contract.Products;
using Sale.Domain.Entities.Products;
using Sale.Domain.Repositories;

namespace Sale.Application.Products.Queries.Products;

internal sealed class ProductsQueryHandler(IProductRepository productRepository)
    : IQueryHandler<ProductsQuery, Maybe<PagedList<ProductResponse>>>
{
    public async Task<Maybe<PagedList<ProductResponse>>> Handle(ProductsQuery request,
        CancellationToken cancellationToken)
    {
        IQueryable<Product> query = productRepository.Queryable(request.SearchText)
            .WhereIf(request.CategoryId.HasValue, x => x.CategoryId == request.CategoryId)
            .Paginate(request.PageNumber, request.PageSize, out Paged paged);

        if (paged.NotExists()) return new PagedList<ProductResponse>(paged);

        List<ProductResponse> data = await query.Include(x => x.Category)
            .Select(x => new ProductResponse(x, false)).ToListAsync(cancellationToken: cancellationToken);

        return new PagedList<ProductResponse>(paged, data);
    }
}