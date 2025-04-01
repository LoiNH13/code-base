using Domain.Core.Primitives.Maybe;
using MediatR;
using Sale.Application.Core.Authentication;
using Sale.Application.Users.Queries.UserById;
using Sale.Contract.Users;

namespace Sale.Application.Authentication.Queries;

internal sealed class ProfileQueryHandler(IUserIdentifierProvider userIdentifierProvider, IMediator mediator)
    : IQueryHandler<ProfileQuery, Maybe<UserResponse>>
{
    public async Task<Maybe<UserResponse>> Handle(ProfileQuery request, CancellationToken cancellationToken)
        => await mediator.Send(new UserByIdQuery(userIdentifierProvider.UserId), cancellationToken);
}