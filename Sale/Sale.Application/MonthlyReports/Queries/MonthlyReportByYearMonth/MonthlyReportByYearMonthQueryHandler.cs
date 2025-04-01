using Application.Core.Extensions;
using Domain.Core.Primitives.Maybe;
using Microsoft.EntityFrameworkCore;
using Sale.Contract.Customers;
using Sale.Domain.Entities.MonthlyReports;
using Sale.Domain.Repositories;

namespace Sale.Application.MonthlyReports.Queries.MonthlyReportByYearMonth;

internal sealed class MonthlyReportByYearMonthQueryHandler : IQueryHandler<MonthlyReportByYearMonthQuery, Maybe<List<CustomerResponse>>>
{
    readonly ICustomerRepository _customerRepository;
    readonly IMonthlyReportRepository _monthlyReportRepository;

    public MonthlyReportByYearMonthQueryHandler(ICustomerRepository customerRepository, IMonthlyReportRepository monthlyReportRepository)
    {
        _customerRepository = customerRepository;
        _monthlyReportRepository = monthlyReportRepository;
    }

    public async Task<Maybe<List<CustomerResponse>>> Handle(MonthlyReportByYearMonthQuery request, CancellationToken cancellationToken)
    {
        var customers = await _customerRepository.Queryable()
            .Where(c => request.CustomerIds.Contains(c.Id))
            .ToListAsync(cancellationToken);

        var monthlyReports = await _monthlyReportRepository.Queryable()
            .Where(r => request.CustomerIds.Contains(r.CustomerId)
                        && r.FromTimeOnUtc.Year == request.Year)
            .WhereIf(request.Month.HasValue, r => r.FromTimeOnUtc.Month == request.Month)
            .Include(r => r.Items)
            .GroupBy(r => new { r.CustomerId, r.FromTimeOnUtc.Month, r.FromTimeOnUtc.Year })
            .Select(g => g.OrderByDescending(r => r.CreatedOnUtc).FirstOrDefault())
            .ToListAsync(cancellationToken);

        var groupedReports = monthlyReports
            .Where(r => r is not null)
            .GroupBy(r => r!.CustomerId)
            .ToList();

        var customerResponses = customers.ConvertAll(customer =>
        {
            List<MonthlyReport> reports = groupedReports
                .Where(r => r.Key == customer.Id)
                .SelectMany(g => g!)
                .ToList()!;
            return CustomerResponse.Create(customer, reports);
        });

        return Maybe<List<CustomerResponse>>.From(customerResponses);
    }
}
