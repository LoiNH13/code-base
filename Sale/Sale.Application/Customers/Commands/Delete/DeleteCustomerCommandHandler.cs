using Application.Core.Abstractions.Data;
using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using Sale.Domain.Core.Errors;
using Sale.Domain.Entities.Customers;
using Sale.Domain.Repositories;

namespace Sale.Application.Customers.Commands.Delete;

internal sealed class DeleteCustomerCommandHandler : ICommandHandler<DeleteCustomerCommand, Result>
{
    readonly ICustomerRepository _customerRepository;
    readonly IUnitOfWork _unitOfWork;

    public DeleteCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        //get customer by id
        Maybe<Customer> mbCustomer = await _customerRepository.GetByIdAsync(request.CustomerId);
        if (mbCustomer.HasNoValue)
            return Result.Failure(SaleDomainErrors.Customer.NotFound);

        Result result = mbCustomer.Value.RemoveCustomer();
        if (result.IsSuccess)
        {
            _customerRepository.Remove(mbCustomer);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return result;
        }
        return result;
    }
}
