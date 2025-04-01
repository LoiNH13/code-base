using Domain.Core.Primitives.Result;

namespace Sale.Application.PlanningApprovals.Commands.Updates.Status.Unlock;

public sealed class UpdateUnlockStatusCommand : ICommand<Result>
{
    public Guid PlanningApprovalId { get; set; }

    public UpdateUnlockStatusCommand(Guid planningApprovalId)
    {
        PlanningApprovalId = planningApprovalId;
    }
}
