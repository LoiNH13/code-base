using Domain.Core.Primitives.Result;

namespace Sale.Application.PlanningControls.Commands.Create;

public sealed class CreatePlanningControlCommand : ICommand<Result>
{
    public string Name { get; }

    public CreatePlanningControlCommand(string name) => Name = name;
}
