using Domain.Core.Events;

namespace Sale.Domain.Events;

public sealed class PlanningCustomerMappingOdooEvent : IDomainEvent
{
    public Guid CustomerId { get; }

    public PlanningCustomerMappingOdooEvent(Guid customerId) => CustomerId = customerId;
}
