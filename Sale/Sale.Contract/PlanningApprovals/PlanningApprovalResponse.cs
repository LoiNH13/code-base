using Sale.Domain.Entities.Planning;

namespace Sale.Contract.PlanningApprovals;

/// <summary>
/// Represents a response for a planning approval.
/// </summary>
public class PlanningApprovalResponse
{
    public Guid Id { get; set; }

    public Guid PlanningControlId { get; set; }

    public Guid CustomerId { get; set; }

    public int Status { get; set; }

    /// <summary>
    /// Gets or sets the status name.
    /// </summary>
    public string StatusName { get; set; }

    public double TotalTargetAmount { get; set; }

    public double TotalOriginalBudgetAmount { get; set; }

    public Guid StatusByUserId { get; set; }

    /// <summary>
    /// Gets the date and time when the planning approval was last modified in UTC.
    /// </summary>
    public DateTime? ModifiedOnUtc { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PlanningApprovalResponse"/> class.
    /// </summary>
    /// <param name="planningApproval">The source planning approval entity.</param>
    public PlanningApprovalResponse(PlanningApproval planningApproval)
    {
        Id = planningApproval.Id;
        PlanningControlId = planningApproval.PlanningControlId;
        CustomerId = planningApproval.CustomerId;
        Status = planningApproval.Status.Value;
        StatusName = planningApproval.Status.Name;
        TotalTargetAmount = planningApproval.TotalTargetAmount;
        TotalOriginalBudgetAmount = planningApproval.TotalOriginalBudgetAmount;
        StatusByUserId = planningApproval.StatusByUserId;
        ModifiedOnUtc = planningApproval.ModifiedOnUtc;
    }
}