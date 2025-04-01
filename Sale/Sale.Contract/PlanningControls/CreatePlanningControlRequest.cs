namespace Sale.Contract.PlanningControls;

/// <summary>
/// Represents a request to create a new planning control.
/// </summary>
public class CreatePlanningControlRequest
{
    /// <summary>
    /// Gets or sets the name of the planning control.
    /// </summary>
    /// <value>
    /// The name of the planning control. This property is required.
    /// </value>
    public required string Name { get; set; }
}