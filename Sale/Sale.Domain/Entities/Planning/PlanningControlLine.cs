using Domain.Core.Abstractions;
using Domain.Core.Primitives;
using Domain.Core.Utility;

namespace Sale.Domain.Entities.Planning;

public sealed class PlanningControlLine : Entity, IAuditableEntity
{
    public PlanningControlLine(PlanningControl planningControl, TimeFrame timeFrame, bool isOriginalBudget, bool isTarget)
    {
        Ensure.NotFalse(() => isOriginalBudget || isTarget, "isOriginalBudget and isTarget cannot be both false", nameof(isOriginalBudget) + " and " + nameof(isTarget));

        PlanningControlId = planningControl.Id;
        TimeFrameId = timeFrame.Id;
        IsOriginalBudget = isOriginalBudget;
        IsTarget = isTarget;
    }

    public Guid PlanningControlId { get; init; }

    public Guid TimeFrameId { get; init; }

    public bool IsOriginalBudget { get; init; }

    public bool IsTarget { get; init; }

    public DateTime CreatedOnUtc { get; }

    public DateTime? ModifiedOnUtc { get; }

    public TimeFrame? TimeFrame { get; }

    private PlanningControlLine()
    {
    }
}
