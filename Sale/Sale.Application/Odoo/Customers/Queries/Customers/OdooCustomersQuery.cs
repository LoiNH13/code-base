using Contract.Common;
using Domain.Core.Primitives.Maybe;
using Sale.Contract.Odoo.Customers;

namespace Sale.Application.Odoo.Customers.Queries.Customers;

public sealed class OdooCustomersQuery : IPagingQuery, IQuery<Maybe<PagedList<OdooCustomerResponse>>>
{
    public OdooCustomersQuery(int pageNumber, int pageSize, string? searchText)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        SearchText = searchText ?? string.Empty;
    }

    public int PageNumber { get; }

    public int PageSize { get; }

    public string SearchText { get; }
}
