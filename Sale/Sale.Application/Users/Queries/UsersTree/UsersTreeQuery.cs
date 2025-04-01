using Domain.Core.Primitives.Maybe;
using Sale.Contract.Users;

namespace Sale.Application.Users.Queries.UsersTree;

public sealed class UsersTreeQuery : IQuery<Maybe<List<UserResponse>>>
{
    public Guid? ParentId { get; set; }

    public UsersTreeQuery(Guid? parentId) => ParentId = parentId;
}
