using Application.Core.Extensions;
using Domain.Core.Primitives.Maybe;
using Microsoft.EntityFrameworkCore;
using Sale.Contract.Customers;
using Sale.Contract.Users;
using Sale.Domain.Entities.Customers;
using Sale.Domain.Entities.Planning;
using Sale.Domain.Repositories;

namespace Sale.Application.Users.Queries.UsersWithPlanApproval;

internal sealed class UsersWithPlanApprovalQueryHandler(
    ICustomerRepository customerRepository,
    IPlanningApprovalRepository planningApprovalRepository)
    : IQueryHandler<UsersWithPlanApprovalQuery, Maybe<List<UsersWithPlanApprovalResponse>>>
{
    public async Task<Maybe<List<UsersWithPlanApprovalResponse>>> Handle(UsersWithPlanApprovalQuery request,
        CancellationToken cancellationToken)
    {
        IQueryable<Customer> query = customerRepository
            .Queryable(string.Empty,
                request.Users.SelectMany(x => x.GetUserIdsIncludeSubordinates()).Distinct().ToList())
            .WhereIf(request.IsHaveOdoo.HasValue,
                x => request.IsHaveOdoo ?? false ? (x.OdooRef ?? 0) > 0 : (x.OdooRef ?? 0) <= 0);

        IQueryable<PlanningApproval> queryPa = planningApprovalRepository.Queryable()
            .Where(x => x.PlanningControlId == request.PlanningControlId);

        var finalQuery = from c in query // truy vấn Customer
                         join pa in queryPa // truy vấn PlanningApproval
                             on c.Id equals pa.CustomerId into planningApprovalGroup
                         from pa in planningApprovalGroup.Take(1).DefaultIfEmpty()
                         where (request.Status.HasNoValue || pa.Status == request.Status)
                         select new { c, pa };

        var groups = await finalQuery.GroupBy(x => x.c.ManagedByUserId, x => x.pa).ToListAsync(cancellationToken);

        List<UsersWithPlanApprovalResponse> response = [];

        foreach (var user in request.Users)
        {
            var data = groups
                .Where(x => user.GetUserIdsIncludeSubordinates().Contains(x.Key ?? Guid.Empty)).SelectMany(x => x)
                .ToList();
            if (data.Count > 0)
            {
                response.Add(new UsersWithPlanApprovalResponse
                {
                    UserId = user.Id,
                    SummaryCustomers = data.GroupBy(x => x?.Status)
                        .Select(g => new SummaryCustomersWithPlanResponse(g.Key,
                            g.Count(),
                            g.Sum(x => x?.TotalTargetAmount ?? 0),
                            g.Sum(x => x?.TotalOriginalBudgetAmount ?? 0)))
                        .ToList()
                });
            }
        }

        return response;
    }
}