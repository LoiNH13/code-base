using Domain.Core.Events;

namespace Sale.Domain.Events;

public sealed class PlanningControlCompletedDomainEvent(Guid planningControlId) : IDomainEvent
{
    public Guid PlanningControlId { get; } = planningControlId;
}