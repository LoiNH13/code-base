using Domain.Core.Primitives.Result;

namespace Sale.Application.PlanningApprovals.Commands.Updates.Status.Approved;

public sealed class UpdateApprovedStatusCommand : ICommand<Result>
{
    public Guid PlanningApprovalId { get; set; }

    public UpdateApprovedStatusCommand(Guid planningApprovalId) => PlanningApprovalId = planningApprovalId;
}
