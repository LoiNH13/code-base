using Application.Core.Extensions;
using Domain.Core.Primitives.Maybe;
using Microsoft.EntityFrameworkCore;
using Sale.Application.Core.Authentication;
using Sale.Contract.Customers;
using Sale.Domain.Entities.Customers;
using Sale.Domain.Entities.Planning;
using Sale.Domain.Enumerations;
using Sale.Domain.Repositories;

namespace Sale.Application.Customers.Queries.CustomersWithPlanApprovals.Summary;

internal sealed class SummaryCustomersWithPlanQueryHandler(
    ICustomerRepository customerRepository,
    IPlanningApprovalRepository planningApprovalRepository,
    IUserIdentifierProvider userIdentifierProvider)
    : IQueryHandler<SummaryCustomersWithPlanQuery, Maybe<List<SummaryCustomersWithPlanResponse>>>
{
    public async Task<Maybe<List<SummaryCustomersWithPlanResponse>>> Handle(SummaryCustomersWithPlanQuery request,
        CancellationToken cancellationToken)
    {
#pragma warning disable CS8604 // Possible null reference argument.
        IQueryable<Customer> query = customerRepository.Queryable(request.SearchText,
                request.Users.SelectMany(x => x.GetUsersIncludeSubordinates()).Select(x => x.Id).ToList(),
                userIdentifierProvider.Role == Domain.Enumerations.ERole.Admin)
            .Include(x => x.PlanNewCustomer)
            .WhereIf(request.CustomerTag == ECustomerTag.PlanNew, x => x.OdooRef == null || x.PlanNewCustomer != default!)
            .WhereIf(request.CustomerTag == ECustomerTag.Opened, x => x.OdooRef != null);
#pragma warning restore CS8604 // Possible null reference argument.

        IQueryable<PlanningApproval> queryPa = planningApprovalRepository.Queryable()
            .Where(x => x.PlanningControlId == request.PlanningControlId);

        //left join with planning approval
        IQueryable<SummaryCustomersWithPlanResponse> finalQuery = from c in query // truy vấn Customer
                                                                  join pa in queryPa // truy vấn PlanningApproval
                                                                      on c.Id equals pa.CustomerId into planningApprovalGroup
                                                                  from pa in planningApprovalGroup.Take(1).DefaultIfEmpty()
                                                                  where (request.Status.HasNoValue || pa.Status == request.Status)
                                                                  //&& pa.Status != default!
                                                                  group pa by pa.Status
            into g
                                                                  select new SummaryCustomersWithPlanResponse(g.Key,
                                                                      g.Count(),
                                                                      g.Sum(x => x.TotalTargetAmount), g.Sum(x => x.TotalOriginalBudgetAmount));

        return await finalQuery.ToListAsync(cancellationToken);
    }
}