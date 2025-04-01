
namespace Sale.Contract.PlanningControlLines;

/// <summary>
/// Represents a request to create a new planning control line.
/// </summary>
public class CreatePlanningControlLineRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the time frame for which the planning control line is being created.
    /// </summary>
    public Guid TimeFrameId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the planning control line is for the original budget.
    /// </summary>
    public bool IsOriginalBudget { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the planning control line is a target.
    /// </summary>
    public bool IsTarget { get; set; }
}