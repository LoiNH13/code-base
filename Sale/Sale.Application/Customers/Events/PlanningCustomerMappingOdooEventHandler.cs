using Application.Core.Abstractions.Common;
using Domain.Core.Events;
using Domain.Core.Exceptions;
using Domain.Core.Primitives.Maybe;
using Microsoft.EntityFrameworkCore;
using Sale.Domain.Core.Errors;
using Sale.Domain.Entities.Customers;
using Sale.Domain.Events;
using Sale.Domain.Repositories;

namespace Sale.Application.Customers.Events;

internal sealed class PlanningCustomerMappingOdooEventHandler : IDomainEventHandler<PlanningCustomerMappingOdooEvent>
{
    readonly ICustomerRepository _customerRepository;
    readonly IDateTime _dateTime;

    public PlanningCustomerMappingOdooEventHandler(ICustomerRepository customerRepository, IDateTime dateTime)
    {
        _customerRepository = customerRepository;
        _dateTime = dateTime;
    }

    public async Task Handle(PlanningCustomerMappingOdooEvent notification, CancellationToken cancellationToken)
    {
        //get customer includes customer timeframes includes timeframes includes metrics includes forecast
        Maybe<Customer> mbCustomers = await _customerRepository.QueryableSplitQuery()
            .Include(c => c.CustomerTimeFrames).ThenInclude(ctf => ctf.TimeFrame)
            .Include(c => c.CustomerTimeFrames).ThenInclude(ctf => ctf.Metrics).ThenInclude(m => m.ForeCast)
            .FirstOrDefaultAsync(x => x.Id == notification.CustomerId) ?? default!;
        if (mbCustomers.HasNoValue) throw new DomainException(SaleDomainErrors.Customer.NotFound);
        mbCustomers.Value.MappingOdooEvent(_dateTime.CurrentConvertMonths);
    }
}
