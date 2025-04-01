using Domain.Core.Primitives.Maybe;
using Sale.Contract.Customers;

namespace Sale.Application.Customers.Queries.CustomerById;

public sealed class CustomerByIdQuery : IQuery<Maybe<CustomerResponse>>
{
    public Guid Id { get; }

    public CustomerByIdQuery(Guid id) => Id = id;
}
