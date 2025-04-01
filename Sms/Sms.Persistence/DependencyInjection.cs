using Application.Core.Abstractions.Data;
using Application.Core.Abstractions.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sms.Application.Core;
using Sms.Domain.Repositories;
using Sms.Persistence.Infrastructure;
using Sms.Persistence.Repositories;
using StackExchange.Redis;

namespace Sms.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddSmsPersistence(this IServiceCollection services, IConfiguration configuration)
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

        services.AddSingleton<IDistributedCache>(sp =>
            sp.GetRequiredService<IDistributedCacheFactory>().CreateCache(Const.CacheInstanceName));

        var connectionString = configuration.GetConnectionString(ConnectionString.SettingsKey)!;

        services.AddSingleton(new ConnectionString(connectionString));

        services.AddDbContext<SmsDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                options.EnableSensitiveDataLogging();
        });

        services.AddScoped<IDbContext>(serviceProvider => serviceProvider.GetRequiredService<SmsDbContext>());

        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<SmsDbContext>());

        services.AddScoped<IResMoRepository, ResMoRepository>();

        services.AddScoped<IMoMessageRepository, MoMessageRepository>();

        return services;
    }
}