using Application.Core.Abstractions.Factories;
using Domain.Core.Events;
using Domain.Events;
using Microsoft.Extensions.Caching.Distributed;
using Sms.Application.Core;
using Sms.Domain.Entities;

namespace Sms.Application.ResMos.Events;

internal sealed class ResMoAddedEventHandler(IDistributedCacheFactory cacheFactory)
    : IDomainEventHandler<EntityAddedEvent<ResMo>>
{
    private readonly IDistributedCache _cache = cacheFactory.CreateCache(Const.CacheInstanceName);

    public async Task Handle(EntityAddedEvent<ResMo> notification, CancellationToken cancellationToken) =>
        await _cache.RemoveAsync(
            $"{notification.Entity.GetType()}:ByServicePhone:{notification.Entity.ServicePhone}",
            cancellationToken);
}