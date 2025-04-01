using Application.Core.Extensions;
using Contract.Common;
using Domain.Core.Primitives.Maybe;
using Microsoft.EntityFrameworkCore;
using Odoo.Domain.Entities;
using Odoo.Domain.Repositories;
using Sale.Contract.Customers;
using Sale.Contract.PlanNewCustomers;
using Sale.Domain.Entities.Customers;
using Sale.Domain.Enumerations;
using Sale.Domain.Repositories;

namespace Sale.Application.Customers.Queries.Customers;

internal sealed class CustomerQueryHandler(
    ICustomerRepository customerRepository,
    IUserRepository userRepository,
    IOdooStateRepository odooStateRepository)
    : IQueryHandler<CustomerQuery, Maybe<PagedList<CustomerResponse>>>
{
    public async Task<Maybe<PagedList<CustomerResponse>>> Handle(CustomerQuery request,
        CancellationToken cancellationToken)
    {
#pragma warning disable CS8604 // Possible null reference argument.
        IQueryable<Customer> query = customerRepository.Queryable(request.SearchText).Include(x => x.PlanNewCustomer)
            .WhereIf(request.UserId.HasValue, x => x.ManagedByUserId == request.UserId)
            .WhereIf(request.NotHaveManaged.HasValue,
                x => (request.NotHaveManaged ?? false) ? x.ManagedByUserId == null : x.ManagedByUserId != null)
            .WhereIf(request.CustomerTag == ECustomerTag.PlanNew, x => x.OdooRef == null || x.PlanNewCustomer != default!)
            .WhereIf(request.CustomerTag == ECustomerTag.Opened, x => x.OdooRef != null)
            .Paginate(request.PageNumber, request.PageSize, out var paged);
#pragma warning restore CS8604 // Possible null reference argument.

        if (paged.NotExists()) return new PagedList<CustomerResponse>(paged);

        var finalQuery = from q in query
                         join user in userRepository.Queryable()
                             on q.ManagedByUserId equals user.Id into userCustomers
                         from user in userCustomers.DefaultIfEmpty()
                         select CustomerResponse.Create(q, user);

        List<CustomerResponse> data = await finalQuery.ToListAsync(cancellationToken);

        foreach (var planNewCustomer in data.Where(x => x.PlanNewCustomer != null).Select(x => x.PlanNewCustomer))
        {
            await LoadStateName(planNewCustomer!);
        }

        return new PagedList<CustomerResponse>(paged, data);
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