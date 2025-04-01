using Application.Core.Abstractions.Data;
using Application.Core.Abstractions.Factories;
using Application.Core.Extensions;
using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Persistence.Extensions;
using Persistence.Repositories;
using Sale.Application.Core;
using Sale.Domain.Core.Errors;
using Sale.Domain.Entities.Users;
using Sale.Domain.Repositories;
using Sale.Persistence.Specifications;

namespace Sale.Persistence.Repositories;

internal sealed class UserRepository(IDbContext dbContext, IDistributedCacheFactory cacheFactory)
    : GenericRepository<User>(dbContext), IUserRepository
{
    private readonly IDistributedCache _cache = cacheFactory.CreateCache(Const.CacheInstanceName);

    public async Task<Maybe<User>> GetByIdAsync(Guid id, bool needAttach)
    {
        Maybe<User> mbUser = await _cache.GetOrCreateAsync(id.ToString(),
            async () => await GetByIdAsync(id).Match(x => x, () => default!),
            new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30) });
        if (mbUser.HasNoValue) return mbUser;
        if (needAttach && !IsEntityAttached(mbUser)) ((DbContext)DbContext).Attach(mbUser.Value);

        return mbUser;
    }

    public async Task<Maybe<User>> GetUserByEmail(Email email) =>
        await DbContext.Set<User>().FirstOrDefaultAsync(new UserWithEmailSpecification(email)) ?? default!;

    public async Task<Result> UserMustNotDependencyLoop(Guid userId, Guid parentId)
    {
        Maybe<User> mbParent = await GetByIdAsync(parentId);

        if (mbParent.HasNoValue) return Result.Failure(SaleDomainErrors.User.NotFound);
        if (mbParent.Value.ManagedByUserId == userId) return Result.Failure(SaleDomainErrors.User.DependencyLoop);
        if (mbParent.Value.ManagedByUserId is null) return Result.Success();
        return await UserMustNotDependencyLoop(userId, mbParent.Value.ManagedByUserId.Value);
    }

    public void LoadAllChilds(User user)
    {
        ((DbContext)DbContext).Entry(user)
            .Collection(x => x.SubordinateUsers).Load();
        foreach (var subordinate in user.SubordinateUsers)
        {
            LoadAllChilds(subordinate);
        }
    }

    public IQueryable<User> Queryable(string searchText) =>
        DbContext.Set<User>().WhereIf(!string.IsNullOrWhiteSpace(searchText),
            new UserWithSearchTextSpecification(searchText));
}