using Application.Core.Extensions;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Persistence.Infrastructure;

namespace Persistence.Extensions;

public static class DistributedCacheExtensions
{
    private static readonly SemaphoreSlim Semaphore = new SemaphoreSlim(1, 1);

    private static DistributedCacheEntryOptions DefaultExpiration => new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
    };

    public static async Task<T> GetOrCreateAsync<T>(
        this IDistributedCache cache,
        string key,
        Func<Task<T>> factory,
        DistributedCacheEntryOptions? cacheOptions = null) where T : class
    {
        key = key.GetKey(typeof(T));
        var cachedData = await cache.GetStringAsync(key);

        if (cachedData is not null)
        {
            return JsonConvert.DeserializeObject<T>(cachedData,
                new JsonSerializerSettings
                {
                    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                    ContractResolver = new PrivateResolver()
                })!;
        }

        T data;
        try
        {
            await Semaphore.WaitAsync();
            data = await factory();

            await cache.SetStringAsync(
                key,
                JsonConvert.SerializeObject(data,
                    new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                cacheOptions ?? DefaultExpiration);
        }
        finally
        {
            Semaphore.Release();
        }

        return data;
    }
}