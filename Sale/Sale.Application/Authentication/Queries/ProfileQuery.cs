using Domain.Core.Primitives.Maybe;
using Sale.Contract.Users;

namespace Sale.Application.Authentication.Queries;

public sealed class ProfileQuery : IQuery<Maybe<UserResponse>>
{
}
