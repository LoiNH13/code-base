using Application.Core.Abstractions.Factories;
using Application.Core.Extensions;
using Domain.Core.Events;
using Domain.Events;
using Microsoft.Extensions.Caching.Distributed;
using Sale.Application.Core;
using Sale.Domain.Entities.Users;

namespace Sale.Application.Users.Events;

internal sealed class UserModifiedEventHandler(IDistributedCacheFactory cacheFactory)
    : IDomainEventHandler<EntityModifiedEvent<User>>
{
    readonly IDistributedCache _cache = cacheFactory.CreateCache(Const.CacheInstanceName);

    public async Task Handle(EntityModifiedEvent<User> notification, CancellationToken cancellationToken) =>
        await _cache.RemoveAsync(notification.Entity.GetKey(), cancellationToken);
}