using Contract.Common;
using Domain.Core.Primitives.Maybe;
using Sale.Contract.Users;

namespace Sale.Application.Users.Queries.Users;

public sealed class UsersQuery : IPagingQuery, IQuery<Maybe<PagedList<UserResponse>>>
{
    public UsersQuery(int pageNumber, int pageSize, string? searchText)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        SearchText = searchText ?? string.Empty;
    }

    public int PageNumber { get; }

    public int PageSize { get; }

    public string SearchText { get; }
}
