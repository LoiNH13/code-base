using Contract.Common;
using Domain.Core.Primitives.Maybe;
using Sale.Contract.Customers;
using Sale.Domain.Enumerations;

namespace Sale.Application.Customers.Queries.Customers;

public sealed class CustomerQuery : IPagingQuery, IQuery<Maybe<PagedList<CustomerResponse>>>
{
    public CustomerQuery(int pageNumber, int pageSize, Guid? userId, string? searchText, ECustomerTag? customerTag, bool? notHaveManaged)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        UserId = userId;
        SearchText = searchText ?? "";
        CustomerTag = customerTag;
        NotHaveManaged = notHaveManaged;
    }

    public int PageNumber { get; }

    public int PageSize { get; }

    public string SearchText { get; }

    public Guid? UserId { get; }

    public ECustomerTag? CustomerTag { get; }

    public bool? NotHaveManaged { get; }
}
