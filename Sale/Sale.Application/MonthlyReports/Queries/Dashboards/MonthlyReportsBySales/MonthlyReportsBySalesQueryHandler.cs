using Domain.Core.Primitives.Maybe;
using Microsoft.EntityFrameworkCore;
using Sale.Contract.MonthlyReports;
using Sale.Domain.Repositories;

namespace Sale.Application.MonthlyReports.Queries.Dashboards.MonthlyReportsBySales;

internal sealed class MonthlyReportsBySalesQueryHandler : IQueryHandler<MonthlyReportsBySalesQuery, Maybe<List<MonthlyReportsBySaleResponse>>>
{
    readonly ICustomerRepository _customerRepository;

    public MonthlyReportsBySalesQueryHandler(ICustomerRepository customerRepository) => _customerRepository = customerRepository;

    public async Task<Maybe<List<MonthlyReportsBySaleResponse>>> Handle(MonthlyReportsBySalesQuery request, CancellationToken cancellationToken)
    {
        var userIds = request.Users.SelectMany(u => u.GetUserIdsIncludeSubordinates()).ToList();
        var customersQuery = await _customerRepository.Queryable("", userIds)
            .Where(x => x.OdooRef != null)
            .Include(x => x.MonthlyReports.Where(mr => (mr.FromTimeOnUtc.Year * 12) + mr.FromTimeOnUtc.Month == request.ConvertMonths)
            .OrderByDescending(mr => mr.CreatedOnUtc).Take(1))
            .ToListAsync(cancellationToken);

        var groups = customersQuery.GroupBy(x => x.ManagedByUserId);

        return request.Users.Select(user =>
        {
            var data = groups.Where(g => user.GetUserIdsIncludeSubordinates().Contains(g.Key ?? Guid.Empty))
                             .SelectMany(g => g)
                             .ToList();
            return new MonthlyReportsBySaleResponse
            {
                UserId = user.Id,
                TotalCustomers = data.Count,
                TotalReports = data.Count(c => c.MonthlyReports.Any())
            };
        }).ToList();
    }
}
