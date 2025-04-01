using Sale.Contract.PlanningControlLines;
using Sale.Domain.Entities.Planning;

namespace Sale.Contract.PlanningControls;

/// <summary>
/// Represents a response model for a Planning Control.
/// </summary>
public class PlanningControlResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the Planning Control.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the Planning Control.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the status of the Planning Control.
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// Gets or sets the list of Planning Control Lines associated with the Planning Control.
    /// </summary>
    public List<PlanningControlLineResponse>? PlanningControlLines { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PlanningControlResponse"/> class based on the provided <see cref="PlanningControl"/> entity.
    /// </summary>
    /// <param name="planningControl">The <see cref="PlanningControl"/> entity to initialize from.</param>
    public PlanningControlResponse(PlanningControl planningControl)
    {
        Id = planningControl.Id;
        Name = planningControl.Name;
        Status = planningControl.Status.Value;
        PlanningControlLines = planningControl.PlanningControlLines.Select(x => new PlanningControlLineResponse(x)).ToList();
    }
}