using Application.Core.Abstractions.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Odoo.Domain.Repositories;
using Odoo.Persistence.Infrastructure;
using Odoo.Persistence.Repositories;
using Odoo.Persistence.Tasks;
using StackExchange.Redis;

namespace Odoo.Persistence;

public static class DependencyInjection
{
    /// <summary>
    ///     Registers the necessary services with the DI framework.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>The same service collection.</returns>
    public static IServiceCollection AddOdooPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RedisCacheOptions>(Const.CacheInstanceName, options =>
        {
            options.ConfigurationOptions = new ConfigurationOptions
            {
                EndPoints = { configuration["Redis:Cache:ConnectionString"]! },
                Password = configuration["Redis:Cache:Password"],
                DefaultDatabase = int.Parse(configuration["Redis:Cache:Database"]!),
            };
            options.InstanceName = Const.CacheInstanceName;
        });

        services.AddSingleton(sp =>
            sp.GetRequiredService<IDistributedCacheFactory>().CreateCache(Const.CacheInstanceName));

        string connectionString = configuration.GetConnectionString(ConnectionString.SettingsKey)!;

        services.AddSingleton(new ConnectionString(connectionString));

        services.AddDbContext<OdooDbContext>(options =>
        {
            options.UseNpgsql(connectionString)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        services.AddHostedService<LoadOdooSchemaBackgroundService>();

        services.AddScoped<IOdooCustomerRepository, OdooCustomerRepository>();

        services.AddScoped<IOdooCategoryRepository, OdooCategoryRepository>();

        services.AddScoped<IOdooProductRepository, OdooProductRepository>();

        services.AddScoped<IOdooStateRepository, OdooStateRepository>();

        services.AddScoped<IOdooDistrictRepository, OdooDistrictRepository>();

        services.AddScoped<IOdooWardRepository, OdooWardRepository>();

        services.AddScoped<IOdooOrderRepository, OdooOrderRepository>();

        services.AddScoped<IOdooAccountJournalRepository, OdooAccountJournalRepository>();

        services.AddScoped<IOdooAccountMoveRepository, OdooAccountMoveRepository>();

        return services;
    }
}