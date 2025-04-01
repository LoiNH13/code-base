using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using Domain.ValueObjects;
using Sale.Domain.Entities.Users;

namespace Sale.Domain.Repositories;

public interface IUserRepository
{
    IQueryable<User> Queryable();

    IQueryable<User> Queryable(string searchText);

    void Insert(User user);

    Task<Maybe<User>> GetUserByEmail(Email email);

    Task<Maybe<User>> GetByIdAsync(Guid id, bool needAttach);

    /// <summary>
    /// Check if user has dependency loop
    /// </summary>
    /// <param name="userId">user to check</param>
    /// <param name="parentId">parent user to check</param>
    /// <returns>true if not loop</returns>
    Task<Result> UserMustNotDependencyLoop(Guid userId, Guid parentId);

    void LoadAllChilds(User user);
}
