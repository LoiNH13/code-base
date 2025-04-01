using Application.Core.Abstractions.Data;
using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using Odoo.Domain.Core.Errors;
using Odoo.Domain.Repositories;
using Sale.Contract.Odoo.Customers;
using Sale.Domain.Entities.Customers;
using Sale.Domain.Repositories;

namespace Sale.Application.Customers.Commands.Creates.Odoo;

internal sealed class CreateOdooCustomerCommandHandler(
    IOdooCustomerRepository odooCustomerRepository,
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateOdooCustomerCommand, Result>
{
    public async Task<Result> Handle(CreateOdooCustomerCommand request, CancellationToken cancellationToken)
    {
        Maybe<OdooCustomerResponse> odooCustomerResponse = await odooCustomerRepository.GetCustomerById(request.OdooRef)
            .Match(x => Maybe<OdooCustomerResponse>.From(new OdooCustomerResponse
            {
                Id = x.Id,
                Name = x.Name,
                InternalRef = x.Ref
            }), () => Maybe<OdooCustomerResponse>.None);
        if (odooCustomerResponse.HasNoValue) return Result.Failure(OdooDomainErrors.Customer.NotFound);

        var customer = new Customer(request.OdooRef, odooCustomerResponse.Value.DTWithName,
            request.BusinessType,
            request.ManagedByUserId,
            request.VisitPerMonth);

        customerRepository.Insert(customer);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}