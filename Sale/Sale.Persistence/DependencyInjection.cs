using Application.Core.Abstractions.Data;
using Application.Core.Abstractions.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sale.Application.Core;
using Sale.Domain.Repositories;
using Sale.Persistence.Infrastructure;
using Sale.Persistence.Repositories;
using StackExchange.Redis;

namespace Sale.Persistence;

public static class DependencyInjection
{
    /// <summary>
    /// Registers the necessary services with the DI framework.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>The same service collection.</returns>
    public static IServiceCollection AddSalePersistence(this IServiceCollection services, IConfiguration configuration)
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

        string connectionString = configuration.GetConnectionString(ConnectionString.SettingsKey)!;

        services.AddSingleton(new ConnectionString(connectionString));

        services.AddDbContext<SaleDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                options.EnableSensitiveDataLogging();
            }
        });

        services.AddScoped<IDbContext>(serviceProvider => serviceProvider.GetRequiredService<SaleDbContext>());

        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<SaleDbContext>());

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<ICategoryRepository, CategoryRepository>();

        services.AddScoped<ICustomerRepository, CustomerRepository>();

        services.AddScoped<IMonthlyReportRepository, MonthlyReportRepository>();

        services.AddScoped<IMonthlyReportRepositoryItem, MonthlyReportRepositoryItem>();

        services.AddScoped<ITimeFrameRepository, TimeFrameRepository>();

        services.AddScoped<IProductRepository, ProductRepository>();

        services.AddScoped<IPlanningControlRepository, PlanningControlRepository>();

        services.AddScoped<IPlanningControlLineRepository, PlanningControlLineRepository>();

        services.AddScoped<IPlanningApprovalRepository, PlanningApprovalRepository>();

        return services;
    }
}
