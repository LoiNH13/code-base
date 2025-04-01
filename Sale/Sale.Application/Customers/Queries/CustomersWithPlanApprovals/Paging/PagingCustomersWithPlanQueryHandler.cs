using Application.Core.Extensions;
using Contract.Common;
using Domain.Core.Primitives.Maybe;
using Microsoft.EntityFrameworkCore;
using Odoo.Domain.Entities;
using Odoo.Domain.Repositories;
using Sale.Application.Core.Authentication;
using Sale.Contract.Customers;
using Sale.Contract.PlanNewCustomers;
using Sale.Contract.PlanningApprovals;
using Sale.Domain.Entities.Customers;
using Sale.Domain.Entities.Planning;
using Sale.Domain.Enumerations;
using Sale.Domain.Repositories;

namespace Sale.Application.Customers.Queries.CustomersWithPlanApprovals.Paging;

internal sealed class PagingCustomersWithPlanQueryHandler(
    ICustomerRepository customerRepository,
    IUserIdentifierProvider userIdentifierProvider,
    IPlanningApprovalRepository planningApprovalRepository,
    IOdooStateRepository odooStateRepository)
    : IQueryHandler<PagingCustomersWithPlanQuery, Maybe<PagedList<CustomerWithPlanResponse>>>
{
    public async Task<Maybe<PagedList<CustomerWithPlanResponse>>> Handle(PagingCustomersWithPlanQuery request,
        CancellationToken cancellationToken)
    {
#pragma warning disable CS8604 // Possible null reference argument.
        IQueryable<Customer> query = customerRepository.Queryable(request.SearchText ?? string.Empty,
                request.Users.SelectMany(x => x.GetUsersIncludeSubordinates().Select(s => s.Id)).ToList(),
                userIdentifierProvider.Role == Domain.Enumerations.ERole.Admin)
            .Include(x => x.PlanNewCustomer)
            .WhereIf(request.CustomerTag == ECustomerTag.PlanNew, x => x.OdooRef == null || x.PlanNewCustomer != default!)
            .WhereIf(request.CustomerTag == ECustomerTag.Opened, x => x.OdooRef != null);
#pragma warning restore CS8604 // Possible null reference argument.

        IQueryable<PlanningApproval> queryPa = planningApprovalRepository.Queryable()
            .Where(x => x.PlanningControlId == request.PlanningControlId);

        //left join with planning approval
        IQueryable<CustomerWithPlanResponse> finalQuery = from c in query // truy vấn Customer
                                                          join pa in queryPa // truy vấn PlanningApproval
                                                              on c.Id equals pa.CustomerId into planningApprovalGroup
                                                          from pa in planningApprovalGroup.Take(1).DefaultIfEmpty()
                                                          where request.Status.HasNoValue || pa.Status == request.Status
                                                          orderby pa.ModifiedOnUtc ?? c.ModifiedOnUtc ?? c.CreatedOnUtc descending, pa.Status
                                                          select new CustomerWithPlanResponse
                                                          {
                                                              Id = c.Id,
                                                              OdooRef = c.OdooRef,
                                                              Name = c.Name,
                                                              ManagedByUserId = c.ManagedByUserId,
                                                              PlanNewCustomer = c.PlanNewCustomer! != default!
                                                                  ? new PlanNewCustomerResponse(c.PlanNewCustomer)
                                                                  : default!,
                                                              PlanningApproval = pa != default! ? new PlanningApprovalResponse(pa) : default!,
                                                          };

        finalQuery = finalQuery.PaginateNotEntity(request.PageNumber, request.PageSize, out var paged);
        if (paged.NotExists()) return new PagedList<CustomerWithPlanResponse>(paged);

        List<CustomerWithPlanResponse> data = await finalQuery.ToListAsync(cancellationToken);

        foreach (var planNewCustomer in data.Where(x => x.PlanNewCustomer != null).Select(x => x.PlanNewCustomer))
        {
            await LoadStateName(planNewCustomer!);
        }

        return new PagedList<CustomerWithPlanResponse>(paged, data);
    }

    private async Task LoadStateName(PlanNewCustomerResponse planNewCustomer)
    {
        var mbState = await odooStateRepository.GetByIdAndIncludeAllAsync(planNewCustomer.CityId);
        if (mbState.HasValue)
        {
            Maybe<ResDistrict> mbDistrict = Maybe<ResDistrict>.None;
            Maybe<ResWard> mbWard = Maybe<ResWard>.None;
            if (planNewCustomer.DistrictId.HasValue)
            {
                mbDistrict =
                    mbState.Value.ResDistricts.FirstOrDefault(f => f.Id == planNewCustomer.DistrictId) ??
                    default!;

                if (mbDistrict.HasValue && planNewCustomer.WardId.HasValue)
                {
                    mbWard =
                        mbDistrict.Value.ResWards.FirstOrDefault(f => f.Id == planNewCustomer.WardId) ??
                        default!;
                }
            }

            planNewCustomer.AddName(mbState.Value.Name,
                mbDistrict.HasValue ? mbDistrict.Value.Name : default,
                mbWard.HasValue ? mbWard.Value.Name : default);
        }
    }
}