using Sale.Domain.Enumerations;

namespace Sale.Contract.Customers;

/// <summary>
/// Represents a summary of customers with their respective planning approval status.
/// </summary>
public class SummaryCustomersWithPlanResponse
{
    /// <summary>
    /// Gets or sets the status value.
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// Gets or sets the status name.
    /// </summary>
    public string StatusName { get; set; }

    /// <summary>
    /// Gets or sets the count of customers with the given status.
    /// </summary>
    public int StatusCount { get; set; }

    /// <summary>
    /// Gets or sets the total target amount for customers with the given status.
    /// </summary>
    public double TotalTargetAmount { get; set; }

    /// <summary>
    /// Gets or sets the total original budget amount for customers with the given status.
    /// </summary>
    public double TotalOriginalBudgetAmount { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SummaryCustomersWithPlanResponse"/> class.
    /// </summary>
    /// <param name="status">The planning approval status.</param>
    /// <param name="statusCount">The count of customers with the given status.</param>
    /// <param name="totalTargetAmount">The total target amount for customers with the given status.</param>
    /// <param name="totalOriginalBudgetAmount">The total original budget amount for customers with the given status.</param>
    public SummaryCustomersWithPlanResponse(PlanningApprovalStatus? status, int statusCount, double totalTargetAmount,
        double totalOriginalBudgetAmount)
    {
        Status = status?.Value ?? 0;
        StatusName = status?.Name ?? "Không có Plan";
        StatusCount = statusCount;
        TotalTargetAmount = totalTargetAmount;
        TotalOriginalBudgetAmount = totalOriginalBudgetAmount;
    }
}