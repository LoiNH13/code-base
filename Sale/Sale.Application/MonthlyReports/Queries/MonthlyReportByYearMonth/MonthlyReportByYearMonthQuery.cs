using Domain.Core.Primitives.Maybe;
using Sale.Contract.Customers;

namespace Sale.Application.MonthlyReports.Queries.MonthlyReportByYearMonth;

public sealed class MonthlyReportByYearMonthQuery : IQuery<Maybe<List<CustomerResponse>>>
{
    public MonthlyReportByYearMonthQuery(List<Guid> customerIds, int year, int? month)
    {
        CustomerIds = customerIds;
        Year = year;
        Month = month;
    }

    public List<Guid> CustomerIds { get; }

    public int Year { get; }

    public int? Month { get; }

}
