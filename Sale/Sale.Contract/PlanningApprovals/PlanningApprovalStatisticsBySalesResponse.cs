using Sale.Contract.Customers;

namespace Sale.Contract.PlanningApprovals;

public class PlanningApprovalStatisticsBySalesResponse
{
    public Guid UserId { get; set; }

    public List<SummaryCustomersWithPlanResponse> SummaryOldCustomers { get; set; } = new();

    public List<SummaryCustomersWithPlanResponse> SummaryCustomers { get; set; } = new();
}