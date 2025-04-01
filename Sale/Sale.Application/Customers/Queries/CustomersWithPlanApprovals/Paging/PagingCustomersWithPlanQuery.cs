using Contract.Common;
using Domain.Core.Primitives.Maybe;
using Sale.Contract.Core;
using Sale.Contract.Customers;
using Sale.Domain.Enumerations;

namespace Sale.Application.Customers.Queries.CustomersWithPlanApprovals.Paging;

public sealed class PagingCustomersWithPlanQuery : ManagedByFilter, IPagingQuery, IQuery<Maybe<PagedList<CustomerWithPlanResponse>>>
{
    public int PageNumber { get; }

    public int PageSize { get; }

    public Guid PlanningControlId { get; }

    public Maybe<PlanningApprovalStatus> Status { get; }

    public string? SearchText { get; }

    public ECustomerTag? CustomerTag { get; }

    public PagingCustomersWithPlanQuery(Guid planningControlId,
                                          int pageNumber,
                                          int pageSize,
                                          Guid? managedByUserId,
                                          int? status,
                                          string? searchText,
                                          ECustomerTag? customerTag) : base(managedByUserId)
    {
        PlanningControlId = planningControlId;
        PageNumber = pageNumber;
        PageSize = pageSize;
        Status = PlanningApprovalStatus.FromValue(status ?? 0);
        SearchText = searchText;
        CustomerTag = customerTag;
    }
}
