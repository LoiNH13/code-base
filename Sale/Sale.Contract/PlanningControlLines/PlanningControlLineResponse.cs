using Sale.Contract.TimeFrames;
using Sale.Domain.Entities.Planning;

namespace Sale.Contract.PlanningControlLines;

/// <summary>
/// Represents a response model for a Planning Control Line.
/// </summary>
public class PlanningControlLineResponse
{
    public Guid Id { get; set; }

    public Guid? TimeFrameId { get; set; }

    public bool IsOriginalBudget { get; }

    public bool IsTarget { get; }

    public TimeFrameResponse? TimeFrame { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PlanningControlLineResponse"/> class.
    /// </summary>
    /// <param name="planningControlLine">The domain entity representing a Planning Control Line.</param>
    public PlanningControlLineResponse(PlanningControlLine planningControlLine)
    {
        Id = planningControlLine.Id;
        if (planningControlLine.TimeFrame is null) TimeFrameId = planningControlLine.TimeFrameId;
        else TimeFrame = new TimeFrameResponse(planningControlLine.TimeFrame);
        IsTarget = planningControlLine.IsTarget;
        IsOriginalBudget = planningControlLine.IsOriginalBudget;
    }
}
