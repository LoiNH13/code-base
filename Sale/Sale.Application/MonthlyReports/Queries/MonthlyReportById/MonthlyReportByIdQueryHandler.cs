using Domain.Core.Primitives.Maybe;
using Microsoft.EntityFrameworkCore;
using Sale.Contract.MonthlyReports;
using Sale.Domain.Entities.MonthlyReports;
using Sale.Domain.Repositories;

namespace Sale.Application.MonthlyReports.Queries.MonthlyReportById;

internal sealed class MonthlyReportByIdQueryHandler(
    IMonthlyReportRepository monthlyReportRepository,
    ICustomerRepository customerRepository)
    : IQueryHandler<MonthlyReportByIdQuery, Maybe<MonthlyReportResponse>>
{
    public async Task<Maybe<MonthlyReportResponse>> Handle(MonthlyReportByIdQuery request,
        CancellationToken cancellationToken)
    {
        IQueryable<MonthlyReport> query = monthlyReportRepository.Queryable()
            .Where(x => x.Id == request.MonthlyReportId)
            .Include(x => x.Items);

        return (await (from q in query
                       join cus in customerRepository.Queryable() on q.CustomerId equals cus.Id
                       select MonthlyReportResponse.Create(q, cus)).FirstOrDefaultAsync(cancellationToken))!;
    }
}