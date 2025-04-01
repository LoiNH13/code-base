using Application.Core.Extensions;
using Domain.Core.Primitives.Maybe;
using Microsoft.EntityFrameworkCore;
using Sale.Application.Core.Authentication;
using Sale.Contract.Users;
using Sale.Domain.Entities.Users;
using Sale.Domain.Repositories;

namespace Sale.Application.Users.Queries.UsersTree;

internal sealed class UsersTreeQueryHandler(
    IUserRepository userRepository,
    IUserIdentifierProvider userIdentifierProvider)
    : IQueryHandler<UsersTreeQuery, Maybe<List<UserResponse>>>
{
    public async Task<Maybe<List<UserResponse>>> Handle(UsersTreeQuery request, CancellationToken cancellationToken)
    {
        Maybe<List<User>> mbUsers = await userRepository.Queryable()
            .WhereIf(request.ParentId.HasValue, x => x.ManagedByUserId == request.ParentId)
            .WhereIf(!request.ParentId.HasValue,
                x => x.ManagedByUserId == (userIdentifierProvider.Role == Domain.Enumerations.ERole.Admin
                    ? null
                    : userIdentifierProvider.UserId))
            .ToListAsync(cancellationToken);
        if (mbUsers.HasNoValue) return new List<UserResponse>();

        mbUsers.Value.ForEach(userRepository.LoadAllChilds);

        return mbUsers.Value.Select(UserResponse.CreateTree).ToList();
    }
}