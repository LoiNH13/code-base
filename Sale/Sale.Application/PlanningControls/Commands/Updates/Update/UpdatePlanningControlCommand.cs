using Domain.Core.Primitives.Result;

namespace Sale.Application.PlanningControls.Commands.Updates.Update;

public sealed class UpdatePlanningControlCommand : ICommand<Result>
{
    public UpdatePlanningControlCommand(Guid planningControlId, string name)
    {
        PlanningControlId = planningControlId;
        Name = name;
    }

    public Guid PlanningControlId { get; }

    public string Name { get; }
}
