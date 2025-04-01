using Background;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sale.Background.Jobs;
using Sale.Background.Tasks;

namespace Sale.Background;

public static class DependencyInjection
{
    public static IServiceCollection AddSaleBackgroundJob(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddBackgroundJob(configuration, "SaleBackgroundJobs:");

        services.AddScoped<CustomerSyncOdoo>();

        services.AddHostedService<RecurringJobHostedServices>();

        return services;
    }
}