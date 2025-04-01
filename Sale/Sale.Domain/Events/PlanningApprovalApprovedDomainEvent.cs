using Domain.Core.Events;
using Sale.Domain.Entities.Customers;
using Sale.Domain.Entities.Planning;

namespace Sale.Domain.Events;

public sealed class PlanningApprovalApprovedDomainEvent(Customer customer, PlanningControl planningControl)
    : IDomainEvent
{
    public Customer Customer { get; } = customer;

    public PlanningControl PlanningControl { get; } = planningControl;
}