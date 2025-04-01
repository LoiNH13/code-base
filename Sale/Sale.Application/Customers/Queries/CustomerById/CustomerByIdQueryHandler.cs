using Domain.Core.Primitives.Maybe;
using Sale.Contract.Customers;
using Sale.Domain.Repositories;

namespace Sale.Application.Customers.Queries.CustomerById;

internal sealed class CustomerByIdQueryHandler(ICustomerRepository customerRepository)
    : IQueryHandler<CustomerByIdQuery, Maybe<CustomerResponse>>
{
    public async Task<Maybe<CustomerResponse>> Handle(CustomerByIdQuery request, CancellationToken cancellationToken)
        => await customerRepository
            .GetByIdAsync(request.Id)
            .Match(x => new CustomerResponse(x), () => default!);
}