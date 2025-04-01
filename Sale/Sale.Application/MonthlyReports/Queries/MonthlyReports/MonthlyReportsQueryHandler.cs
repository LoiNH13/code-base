using Application.Core.Extensions;
using Contract.Common;
using Domain.Core.Primitives.Maybe;
using Microsoft.EntityFrameworkCore;
using Sale.Contract.MonthlyReports;
using Sale.Domain.Entities.MonthlyReports;
using Sale.Domain.Repositories;

namespace Sale.Application.MonthlyReports.Queries.MonthlyReports;

internal sealed class
    MonthlyReportsQueryHandler(
        IMonthlyReportRepository monthlyReportRepository,
        ICustomerRepository customerRepository,
        IUserRepository userRepository)
    : IQueryHandler<MonthlyReportsQuery, Maybe<PagedList<MonthlyReportResponse>>>
{
    public async Task<Maybe<PagedList<MonthlyReportResponse>>> Handle(MonthlyReportsQuery request,
        CancellationToken cancellationToken)
    {
        IQueryable<MonthlyReport> query = monthlyReportRepository.Queryable()
            .WhereIf(request.CreatedBy.HasValue, x => x.CreateByUser == request.CreatedBy)
            .WhereIf(request.CustomerId.HasValue, x => x.CustomerId == request.CustomerId)
            .Paginate(request.PageNumber, request.PageSize, out Paged paged);

        if (paged.NotExists()) return new PagedList<MonthlyReportResponse>(paged);

        List<MonthlyReportResponse> data = await (from q in query
                                                  join c in customerRepository.Queryable()
                                                      on q.CustomerId equals c.Id
                                                  join u in userRepository.Queryable()
                                                      on q.CreateByUser equals u.Id
                                                  select MonthlyReportResponse.Create(q, c, u)).ToListAsync(cancellationToken);

        return new PagedList<MonthlyReportResponse>(paged, data);
    }
}