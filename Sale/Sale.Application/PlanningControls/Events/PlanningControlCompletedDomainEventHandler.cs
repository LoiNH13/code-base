using Domain.Core.Events;
using Microsoft.EntityFrameworkCore;
using Sale.Application.Core.Authentication;
using Sale.Domain.Entities.Planning;
using Sale.Domain.Enumerations;
using Sale.Domain.Events;
using Sale.Domain.Repositories;

namespace Sale.Application.PlanningControls.Events;

internal sealed class PlanningControlCompletedDomainEventHandler(
    IPlanningApprovalRepository approvalRepository,
    IUserIdentifierProvider userIdentifierProvider)
    : IDomainEventHandler<PlanningControlCompletedDomainEvent>
{
    public async Task Handle(PlanningControlCompletedDomainEvent notification, CancellationToken cancellationToken)
    {
        List<PlanningApproval> planningApprovals = await approvalRepository.Queryable()
            .Where(x => x.PlanningControlId == notification.PlanningControlId)
            .Where(x => x.Status == PlanningApprovalStatus.Approved).ToListAsync(cancellationToken);

        planningApprovals.ForEach(x => x.LockStatus(userIdentifierProvider.UserId));
    }
}