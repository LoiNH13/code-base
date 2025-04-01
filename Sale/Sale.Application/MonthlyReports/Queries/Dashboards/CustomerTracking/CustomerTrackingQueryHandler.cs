using Application.Core.Extensions;
using Contract.Common;
using Domain.Core.Primitives.Maybe;
using Microsoft.EntityFrameworkCore;
using Sale.Contract.Customers;
using Sale.Domain.Entities.Customers;
using Sale.Domain.Repositories;

namespace Sale.Application.MonthlyReports.Queries.Dashboards.CustomerTracking;

internal sealed class CustomerTrackingQueryHandler : IQueryHandler<CustomerTrackingQuery, Maybe<PagedList<CustomerResponse>>>
{
    readonly ICustomerRepository _customerRepository;

    public CustomerTrackingQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Maybe<PagedList<CustomerResponse>>> Handle(CustomerTrackingQuery request, CancellationToken cancellationToken)
    {
        var userIds = request.Users.SelectMany(u => u.GetUserIdsIncludeSubordinates()).ToList();
        IQueryable<Customer> customersQuery = _customerRepository.Queryable("", userIds)
            .Where(x => x.OdooRef != null)
            .Where(x => x.MonthlyReports.Any(mr => (mr.FromTimeOnUtc.Year * 12) + mr.FromTimeOnUtc.Month == request.ConvertMonths) == request.IsVisited)
            .OrderByDescending(x => x.CreatedOnUtc)
            .Paginate(request.PageNumber, request.PageSize, out Paged paged);

        if (paged.NotExists())
            return new PagedList<CustomerResponse>(paged);

        List<CustomerResponse> data = await customersQuery
            .Select(x => new CustomerResponse(x))
            .ToListAsync(cancellationToken);

        return new PagedList<CustomerResponse>(paged, data);
    }
}
