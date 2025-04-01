using Domain.Core.Primitives.Result;

namespace Sale.Application.PlanningApprovals.Commands.Updates.Status.EditRequest;

public sealed class UpdateEditRequestStatusCommand : ICommand<Result>
{
    public Guid PlanningApprovalId { get; set; }

    public UpdateEditRequestStatusCommand(Guid planningApprovalId)
    {
        PlanningApprovalId = planningApprovalId;
    }
}
