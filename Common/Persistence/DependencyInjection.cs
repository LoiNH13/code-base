using Application.Core.Abstractions.Factories;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Factories;

namespace Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddSingleton<IDistributedCacheFactory, DistributedCacheFactory>();
        return services;
    }
}
