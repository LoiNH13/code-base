using Application.Core.Extensions;
using Contract.Common;
using Domain.Core.Primitives.Maybe;
using Microsoft.EntityFrameworkCore;
using Sale.Contract.Users;
using Sale.Domain.Entities.Users;
using Sale.Domain.Repositories;

namespace Sale.Application.Users.Queries.Users;

internal sealed class UsersQueryHandler(IUserRepository userRepository)
    : IQueryHandler<UsersQuery, Maybe<PagedList<UserResponse>>>
{
    public async Task<Maybe<PagedList<UserResponse>>> Handle(UsersQuery request, CancellationToken cancellationToken)
    {
        IQueryable<User> query = userRepository.Queryable(request.SearchText)
            .Paginate(request.PageNumber, request.PageSize, out Paged paged);
        if (paged.NotExists()) return new PagedList<UserResponse>(paged);

        List<UserResponse> users = await query.Include(x => x.ManagedByUser).Select(x => UserResponse.Create(x))
            .ToListAsync(cancellationToken);
        return new PagedList<UserResponse>(paged, users);
    }
}