using Application.Core.Extensions;
using Domain.Core.Primitives.Maybe;
using Microsoft.EntityFrameworkCore;
using Sale.Contract.Customers;
using Sale.Contract.PlanningApprovals;
using Sale.Domain.Entities.Planning;
using Sale.Domain.Repositories;

namespace Sale.Application.PlanningApprovals.Queries.StatisticsBySales;

internal sealed class PlanningApprovalStatisticsBySalesQueryHandler :
    IQueryHandler<PlanningApprovalStatisticsBySalesQuery, Maybe<List<PlanningApprovalStatisticsBySalesResponse>>>
{
    readonly IPlanningApprovalRepository _planningApprovalRepository;
    readonly ICustomerRepository _customerRepository;

    public PlanningApprovalStatisticsBySalesQueryHandler(IPlanningApprovalRepository planningApprovalRepository, ICustomerRepository customerRepository)
    {
        _planningApprovalRepository = planningApprovalRepository;
        _customerRepository = customerRepository;
    }

    public async Task<Maybe<List<PlanningApprovalStatisticsBySalesResponse>>> Handle(PlanningApprovalStatisticsBySalesQuery request, CancellationToken cancellationToken)
    {
        var scopeUsers = request.Users.SelectMany(x => x.GetUserIdsIncludeSubordinates()).Distinct().ToList();
        IQueryable<PlanningApproval> query = _planningApprovalRepository.Queryable()
            .Where(x => x.PlanningControlId == request.PlanningControlId)
            .Where(x => scopeUsers.Contains(x.CustomerManagedBy))
            .WhereIf(request.PlanningApprovalStatuses.Count > 0, x => request.PlanningApprovalStatuses.Contains(x.Status));

        var grouped = await query
            .Join(_customerRepository.Queryable(), pa => pa.CustomerId, c => c.Id, (pa, c) => new { pa, c })
            .GroupBy(x => new { x.pa.CustomerManagedBy }, x => new { x.pa, IsOld = x.c.OdooRef != null })
            .ToListAsync(cancellationToken);


        List<PlanningApprovalStatisticsBySalesResponse> response = [];
        foreach (var user in request.Users)
        {
            var userGroup = grouped.Where(x => user.GetUserIdsIncludeSubordinates().Contains(x.Key.CustomerManagedBy))
                .SelectMany(x => x).ToList();
            if (userGroup.Count > 0)
            {
                response.Add(new PlanningApprovalStatisticsBySalesResponse
                {
                    UserId = user.Id,
                    SummaryCustomers = userGroup.GroupBy(x => x.pa.Status)
                        .Select(g => new SummaryCustomersWithPlanResponse(
                            g.Key,
                            g.Count(),
                            g.Sum(x => x.pa.TotalTargetAmount),
                            g.Sum(x => x.pa.TotalOriginalBudgetAmount)))
                        .ToList(),
                    SummaryOldCustomers = userGroup.Where(x => x.IsOld).GroupBy(x => x.pa.Status)
                        .Select(g => new SummaryCustomersWithPlanResponse(
                            g.Key,
                            g.Count(),
                            g.Sum(x => x.pa.TotalTargetAmount),
                            g.Sum(x => x.pa.TotalOriginalBudgetAmount)))
                        .ToList()
                });
            }
        }
        return response;
    }
}
