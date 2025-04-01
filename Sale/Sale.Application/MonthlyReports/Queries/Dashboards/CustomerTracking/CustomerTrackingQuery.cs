using Contract.Common;
using Domain.Core.Primitives.Maybe;
using Sale.Contract.Core;
using Sale.Contract.Customers;

namespace Sale.Application.MonthlyReports.Queries.Dashboards.CustomerTracking;

public sealed class CustomerTrackingQuery : ManagedByFilter, IPagingQuery, IQuery<Maybe<PagedList<CustomerResponse>>>
{
    public CustomerTrackingQuery(int convertMonths, int pageNumber, int pageSize, List<Guid> managedByUserIds, bool includeSubordinateUsers = false, bool isVisited = false)
        : base(managedByUserIds, includeSubordinateUsers)
    {
        ConvertMonths = convertMonths;
        PageNumber = pageNumber;
        PageSize = pageSize;
        IsVisited = isVisited;
    }

    public int ConvertMonths { get; }

    public int PageNumber { get; }

    public int PageSize { get; }

    public bool IsVisited { get; }
}
