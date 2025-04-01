using Domain.Core.Primitives.Maybe;
using Sale.Contract.Core;
using Sale.Contract.MonthlyReports;

namespace Sale.Application.MonthlyReports.Queries.Dashboards.MonthlyReportsBySales;

public sealed class MonthlyReportsBySalesQuery : ManagedByFilter, IQuery<Maybe<List<MonthlyReportsBySaleResponse>>>
{
    public MonthlyReportsBySalesQuery(int convertMonths, List<Guid> managedByUserIds, bool? includeSubordinateUsers)
        : base(managedByUserIds, includeSubordinateUsers ?? true) => ConvertMonths = convertMonths;
    public int ConvertMonths { get; }
}
