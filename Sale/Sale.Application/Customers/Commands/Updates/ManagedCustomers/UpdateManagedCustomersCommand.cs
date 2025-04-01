using Domain.Core.Primitives.Result;

namespace Sale.Application.Customers.Commands.Updates.ManagedCustomers;

public sealed class UpdateManagedCustomersCommand : ICommand<Result>
{
    public UpdateManagedCustomersCommand(Guid? userId, List<Guid> customerIds)
    {
        UserId = userId;
        CustomerIds = customerIds;
    }

    public Guid? UserId { get; }

    public List<Guid> CustomerIds { get; }
}
