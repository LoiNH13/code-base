using Application.Core.Abstractions.Factories;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Persistence.Factories;

public class DistributedCacheFactory : IDistributedCacheFactory
{
    private readonly IServiceProvider _serviceProvider;

    public DistributedCacheFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IDistributedCache CreateCache(string name)
    {
        var options = _serviceProvider.GetRequiredService<IOptionsMonitor<RedisCacheOptions>>().Get(name);
        return new RedisCache(options);
    }
}
