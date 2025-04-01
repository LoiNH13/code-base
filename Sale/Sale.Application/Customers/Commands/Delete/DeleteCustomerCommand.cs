using Domain.Core.Primitives.Result;

namespace Sale.Application.Customers.Commands.Delete;

public sealed class DeleteCustomerCommand : ICommand<Result>
{
    public Guid CustomerId { get; }

    public DeleteCustomerCommand(Guid customerId) => CustomerId = customerId;
}
