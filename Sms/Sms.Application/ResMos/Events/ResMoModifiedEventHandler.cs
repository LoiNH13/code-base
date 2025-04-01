using Application.Core.Abstractions.Factories;
using Domain.Core.Events;
using Domain.Events;
using Microsoft.Extensions.Caching.Distributed;
using Sms.Application.Core;
using Sms.Domain.Entities;

namespace Sms.Application.ResMos.Events;

public class ResMoModifiedEventHandler(IDistributedCacheFactory cacheFactory)
    : IDomainEventHandler<EntityModifiedEvent<ResMo>>
{
    private readonly IDistributedCache _cache = cacheFactory.CreateCache(Const.CacheInstanceName);

    public async Task Handle(EntityModifiedEvent<ResMo> notification, CancellationToken cancellationToken) =>
        await _cache.RemoveAsync($"{notification.Entity.GetType()}:ByServicePhone:{notification.Entity.ServicePhone}",
            cancellationToken);
}