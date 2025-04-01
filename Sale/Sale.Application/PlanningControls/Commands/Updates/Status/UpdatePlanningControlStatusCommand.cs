using Domain.Core.Primitives.Result;

namespace Sale.Application.PlanningControls.Commands.Updates.Status;

public sealed class UpdatePlanningControlStatusCommand : ICommand<Result>
{
    public UpdatePlanningControlStatusCommand(Guid planningControlId, int status)
    {
        PlanningControlId = planningControlId;
        Status = status;
    }

    public Guid PlanningControlId { get; }

    public int Status { get; }
}
