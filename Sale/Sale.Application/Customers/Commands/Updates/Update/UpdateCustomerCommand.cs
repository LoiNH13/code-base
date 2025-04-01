using Domain.Core.Primitives.Result;
using Sale.Domain.Enumerations;

namespace Sale.Application.Customers.Commands.Updates.Update;

public sealed class UpdateCustomerCommand : ICommand<Result>
{
    public UpdateCustomerCommand(Guid customerId, Guid? managedByUserId, int vitsitPerMonth, EBusinessType businessType)
    {
        CustomerId = customerId;
        ManagedByUserId = managedByUserId;
        VitsitPerMonth = vitsitPerMonth;
        BusinessType = businessType;
    }

    public Guid CustomerId { get; }

    public Guid? ManagedByUserId { get; }

    public int VitsitPerMonth { get; }

    public EBusinessType BusinessType { get; }
}
