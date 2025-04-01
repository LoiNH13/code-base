using Application.Core.Abstractions.Data;
using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using Sale.Domain.Core.Errors;
using Sale.Domain.Entities.Customers;
using Sale.Domain.Entities.Users;
using Sale.Domain.Repositories;

namespace Sale.Application.Customers.Commands.Updates.Update;

internal sealed class UpdateCustomerCommandHandler(
    ICustomerRepository customerRepository,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateCustomerCommand, Result>
{
    public async Task<Result> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        Maybe<Customer> mbCustomer = await customerRepository.GetByIdAsync(request.CustomerId);
        if (mbCustomer.HasNoValue) return Result.Failure(SaleDomainErrors.Customer.NotFound);

        Result result;
        if (request.ManagedByUserId.HasValue)
        {
            Maybe<User> mbUser = await userRepository.GetByIdAsync(request.ManagedByUserId ?? Guid.Empty, false);
            if (mbUser.HasNoValue) return Result.Failure(SaleDomainErrors.User.NotFound);
            result = mbCustomer.Value.Update(mbCustomer.Value.Name, mbUser, request.VitsitPerMonth, request.BusinessType);
        }
        else
        {
            result = mbCustomer.Value.Update(mbCustomer.Value.Name, default, request.VitsitPerMonth, request.BusinessType);
        }

        if (result.IsFailure) return result;

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return result;
    }
}