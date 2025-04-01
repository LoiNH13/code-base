using Domain.Core.Primitives.Maybe;
using Sale.Contract.Core;
using Sale.Contract.Customers;
using Sale.Domain.Enumerations;

namespace Sale.Application.Customers.Queries.CustomersWithPlanApprovals.Summary;

public sealed class SummaryCustomersWithPlanQuery : ManagedByFilter,
    IQuery<Maybe<List<SummaryCustomersWithPlanResponse>>>
{
    public Guid PlanningControlId { get; }

    public Maybe<PlanningApprovalStatus> Status { get; }

    public string SearchText { get; }

    public ECustomerTag? CustomerTag { get; }

    public SummaryCustomersWithPlanQuery(Guid planningControlId,
        int? status,
        string? searchText,
        Guid? managedByUserId,
        ECustomerTag? customerTag) : base(managedByUserId)
    {
        PlanningControlId = planningControlId;
        Status = PlanningApprovalStatus.FromValue(status ?? 0);
        SearchText = searchText ?? string.Empty;
        CustomerTag = customerTag;
    }
}