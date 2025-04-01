using Domain.Core.Primitives.Maybe;
using Sale.Contract.Core;
using Sale.Contract.PlanningApprovals;
using Sale.Domain.Enumerations;

namespace Sale.Application.PlanningApprovals.Queries.StatisticsBySales;

public sealed class PlanningApprovalStatisticsBySalesQuery
    : ManagedByFilter, IQuery<Maybe<List<PlanningApprovalStatisticsBySalesResponse>>>
{
    public PlanningApprovalStatisticsBySalesQuery(Guid planningControlId, List<int>? planningApprovalStatuses, List<Guid>? managedByUserIds, bool includeSubordinateUsers = true)
        : base(managedByUserIds, includeSubordinateUsers)
    {
        PlanningControlId = planningControlId;
        //check planningApprovalStatus is null or empty then foreach item
        //after check item is valid PlanningApprovalStatus then add to PlanningApprovalStatus
        if (planningApprovalStatuses != null)
        {
            PlanningApprovalStatuses = (from status in planningApprovalStatuses
                                        where PlanningApprovalStatus.ContainsValue(status)
                                        select new PlanningApprovalStatus(status)).ToList();
        }
    }

    public Guid PlanningControlId { get; }

    public List<PlanningApprovalStatus> PlanningApprovalStatuses { get; } = new();
}
