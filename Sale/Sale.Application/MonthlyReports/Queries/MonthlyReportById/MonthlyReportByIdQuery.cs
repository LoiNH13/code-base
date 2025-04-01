using Domain.Core.Primitives.Maybe;
using Sale.Contract.MonthlyReports;

namespace Sale.Application.MonthlyReports.Queries.MonthlyReportById;

public sealed class MonthlyReportByIdQuery : IQuery<Maybe<MonthlyReportResponse>>
{
    public Guid MonthlyReportId { get; }

    public MonthlyReportByIdQuery(Guid monthlyReportId)
    {
        MonthlyReportId = monthlyReportId;
    }
}
