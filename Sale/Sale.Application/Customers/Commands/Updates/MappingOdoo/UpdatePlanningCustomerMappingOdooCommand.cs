using Domain.Core.Primitives.Result;

namespace Sale.Application.Customers.Commands.Updates.MappingOdoo;

public sealed class UpdatePlanningCustomerMappingOdooCommand : ICommand<Result>
{
    public Guid CustomerId { get; }

    public int OdooRef { get; }

    public UpdatePlanningCustomerMappingOdooCommand(Guid customerId, int odooRef)
    {
        CustomerId = customerId;
        OdooRef = odooRef;
    }
}
