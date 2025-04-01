

namespace Sale.Contract.PlanningApprovals;

public class PlanningApprovalStatisticsBySalesRequest
{
    public Guid PlanningControlId { get; set; }
    public List<Guid>? ManagedByUserIds { get; set; }
    public bool IncludeSubordinateUsers { get; set; }
    public List<int>? PlanningApprovalStatuses { get; set; }
}