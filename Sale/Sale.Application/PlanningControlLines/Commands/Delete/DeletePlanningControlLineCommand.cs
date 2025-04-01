using Domain.Core.Primitives.Result;

namespace Sale.Application.PlanningControlLines.Commands.Delete;

public sealed class DeletePlanningControlLineCommand : ICommand<Result>
{
    public DeletePlanningControlLineCommand(Guid planningControlId, Guid planningControlLineId)
    {
        PlanningControlId = planningControlId;
        PlanningControlLineId = planningControlLineId;
    }

    public Guid PlanningControlId { get; }

    public Guid PlanningControlLineId { get; }
}
