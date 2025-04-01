using Domain.Core.Primitives.Maybe;
using Sale.Contract.Users;
using Sale.Domain.Repositories;

namespace Sale.Application.Users.Queries.UserById;

internal sealed class UserByIdQueryHandler(IUserRepository userRepository)
    : IQueryHandler<UserByIdQuery, Maybe<UserResponse>>
{
    public async Task<Maybe<UserResponse>> Handle(UserByIdQuery request, CancellationToken cancellationToken)
        => await userRepository.GetByIdAsync(request.Id, false).Match(x => new UserResponse(x), () => default!);
}