namespace Sale.Contract.PlanningApprovals;

/// <summary>
/// Represents a request to check the waiting approval status for a specific planning control.
/// </summary>
public class WaitingApprovalStatusRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the customer for whom the waiting approval status is being checked.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the planning control for which the waiting approval status is being checked.
    /// </summary>
    public Guid PlanningControlId { get; set; }
}
