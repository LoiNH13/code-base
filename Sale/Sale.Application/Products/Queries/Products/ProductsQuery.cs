using Contract.Common;
using Domain.Core.Primitives.Maybe;
using Sale.Contract.Products;

namespace Sale.Application.Products.Queries.Products;

public sealed class ProductsQuery : IPagingQuery, IQuery<Maybe<PagedList<ProductResponse>>>
{
    public ProductsQuery(int pageNumber, int pageSize, string? searchText, Guid? categoryId)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        SearchText = searchText ?? "";
        CategoryId = categoryId;
    }

    public int PageNumber { get; }

    public int PageSize { get; }

    public string SearchText { get; }

    public Guid? CategoryId { get; }
}
