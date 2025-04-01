namespace Sale.Contract.PlanningControls;

/// <summary>
/// Represents a request to update the status of a planning control.
/// </summary>
public class UpdatePlanningControlStatusRequest
{
    /// <summary>
    /// Gets or sets the new status for the planning control.
    /// </summary>
    /// <value>
    /// An integer representing the new status.
    /// </value>
    public int Status { get; set; }
}
