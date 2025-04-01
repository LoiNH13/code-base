using Application.Core.Abstractions.Data;
using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using Sale.Domain.Core.Errors;
using Sale.Domain.Entities.Users;
using Sale.Domain.Repositories;

namespace Sale.Application.Users.Commands.Update;

internal sealed class UpdateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateUserCommand, Result>
{
    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        Maybe<User> mbUser = await userRepository.GetByIdAsync(request.Id, true);
        if (mbUser.HasNoValue) return Result.Failure(SaleDomainErrors.User.NotFound);

        await mbUser.Value.Update(request.Name,
                                  request.ManagedByUserId,
                                  request.Role,
                                  request.BusinessType,
                                  userRepository);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}