using Application.Core.Abstractions.Data;
using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using Microsoft.EntityFrameworkCore;
using Sale.Domain.Core.Errors;
using Sale.Domain.Entities.Customers;
using Sale.Domain.Entities.Users;
using Sale.Domain.Repositories;

namespace Sale.Application.Customers.Commands.Updates.ManagedCustomers;

internal sealed class UpdateManagedCustomersCommandHandler(
    IUserRepository userRepository,
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateManagedCustomersCommand, Result>
{
    public async Task<Result> Handle(UpdateManagedCustomersCommand request, CancellationToken cancellationToken)
    {
        if (request.UserId is not null)
        {
            Maybe<User> mbUser = await userRepository.GetByIdAsync(request.UserId.Value, false);
            if (mbUser.HasNoValue) return Result.Failure(SaleDomainErrors.User.NotFound);
        }

        Maybe<List<Customer>> mbCustomers = await customerRepository.Queryable()
            .Where(x => request.CustomerIds.Contains(x.Id)).ToListAsync(cancellationToken);

        foreach (Customer customer in mbCustomers.Value)
        {
            customer.UpdateManagedByUser(request.UserId);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}