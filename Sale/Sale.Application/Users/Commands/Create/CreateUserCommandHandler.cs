using Domain.Core.Primitives.Maybe;
using Sale.Contract.Users;

namespace Sale.Application.Users.Commands.Create;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Maybe<UserResponse>>
{
    public Task<Maybe<UserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
