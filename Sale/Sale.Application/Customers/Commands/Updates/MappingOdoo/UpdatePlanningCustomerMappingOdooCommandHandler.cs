using Application.Core.Abstractions.Data;
using Domain.Core.Exceptions;
using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using Odoo.Domain.Core.Errors;
using Odoo.Domain.Repositories;
using Sale.Contract.Odoo.Customers;
using Sale.Domain.Core.Errors;
using Sale.Domain.Entities.Customers;
using Sale.Domain.Repositories;

namespace Sale.Application.Customers.Commands.Updates.MappingOdoo;

internal class UpdatePlanningCustomerMappingOdooCommandHandler : ICommandHandler<UpdatePlanningCustomerMappingOdooCommand, Result>
{
    readonly ICustomerRepository _customerRepository;
    readonly IOdooCustomerRepository _odooCustomerRepository;
    readonly IUnitOfWork _unitOfWork;

    public UpdatePlanningCustomerMappingOdooCommandHandler(ICustomerRepository customerRepository, IOdooCustomerRepository odooCustomerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _odooCustomerRepository = odooCustomerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdatePlanningCustomerMappingOdooCommand request, CancellationToken cancellationToken)
    {
        //get customer by id from IcustomerRepository
        Maybe<Customer> mbCustomer = await _customerRepository.GetByIdAsync(request.CustomerId);
        if (mbCustomer.HasNoValue)
            return Result.Failure(SaleDomainErrors.Customer.NotFound);

        //get customer by odooRef from IOdooCustomerRepository
        Maybe<OdooCustomerResponse> mbOdooCustomer = await _odooCustomerRepository.GetCustomerById(request.OdooRef)
            .Match(x => new OdooCustomerResponse
            {
                Id = x.Id,
                Name = x.Name,
                InternalRef = x.Ref
            }, () => throw new DomainException(OdooDomainErrors.Customer.NotFound));

        //update customer odooRef
        Result result = await mbCustomer.Value.UpdateOdooRef(request.OdooRef, mbOdooCustomer.Value.DTWithName, _customerRepository);
        if (result.IsSuccess) await _unitOfWork.SaveChangesAsync();
        return result;
    }
}
