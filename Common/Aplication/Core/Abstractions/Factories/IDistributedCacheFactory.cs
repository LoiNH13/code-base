using Microsoft.Extensions.Caching.Distributed;

namespace Application.Core.Abstractions.Factories;

public interface IDistributedCacheFactory
{
    IDistributedCache CreateCache(string name);
}
