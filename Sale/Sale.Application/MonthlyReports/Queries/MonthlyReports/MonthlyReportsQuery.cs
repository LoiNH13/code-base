using Contract.Common;
using Domain.Core.Primitives.Maybe;
using Sale.Contract.MonthlyReports;

namespace Sale.Application.MonthlyReports.Queries.MonthlyReports;

public sealed class MonthlyReportsQuery : IPagingQuery, IQuery<Maybe<PagedList<MonthlyReportResponse>>>
{
    public MonthlyReportsQuery(int pageNumber, int pageSize, Guid? createdBy, Guid? customerId)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        CreatedBy = createdBy;
        CustomerId = customerId;
    }

    public int PageNumber { get; }

    public int PageSize { get; }

    public Guid? CustomerId { get; }

    public Guid? CreatedBy { get; }
}