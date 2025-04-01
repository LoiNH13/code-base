using Domain.Core.Primitives.Result;

namespace Sale.Application.PlanningControlLines.Commands.Create;

public sealed class CreatePlanningControlLineCommand : ICommand<Result>
{
    public CreatePlanningControlLineCommand(Guid planningControlId, Guid timeFrameId, bool isOriginalBudget, bool isTarget)
    {
        PlanningControlId = planningControlId;
        TimeFrameId = timeFrameId;
        IsOriginalBudget = isOriginalBudget;
        IsTarget = isTarget;
    }

    public Guid PlanningControlId { get; }

    public Guid TimeFrameId { get; }

    public bool IsOriginalBudget { get; }

    public bool IsTarget { get; }
}
