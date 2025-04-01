using Contract.Common;
using Domain.Core.Primitives.Maybe;
using Sale.Contract.Odoo.Products;

namespace Sale.Application.Odoo.Products.Queries.Products;

public sealed class OdooProductsQuery : IPagingQuery, IQuery<Maybe<PagedList<OdooProductResponse>>>
{
    public OdooProductsQuery(int pageNumber, int pageSize, string? searchText)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        SearchText = searchText ?? string.Empty;
    }

    public int PageNumber { get; }

    public int PageSize { get; }

    public string SearchText { get; }
}
