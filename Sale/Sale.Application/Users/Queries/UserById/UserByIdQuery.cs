using Domain.Core.Primitives.Maybe;
using Sale.Contract.Users;

namespace Sale.Application.Users.Queries.UserById;

public sealed class UserByIdQuery : IQuery<Maybe<UserResponse>>
{
    public Guid Id { get; }

    public UserByIdQuery(Guid id) => Id = id;
}
