using Domain.Core.Primitives.Result;

namespace Sale.Application.PlanningApprovals.Commands.Updates.Status.WaitingForApprove;

public sealed class UpdateWaitingForApproveStatusCommand : ICommand<Result>
{
    public UpdateWaitingForApproveStatusCommand(Guid customerId, Guid planningControlId)
    {
        CustomerId = customerId;
        PlanningControlId = planningControlId;
    }

    public Guid CustomerId { get; }

    public Guid PlanningControlId { get; }
}
