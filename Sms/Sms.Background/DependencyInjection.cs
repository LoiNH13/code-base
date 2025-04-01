using Background.Services;
using Integration.Kafka.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sms.Background.Services;
using Sms.Background.Tasks;
using System.Reflection;

namespace Sms.Background;

public static class DependencyInjection
{
    public static IServiceCollection AddSmsBackgroundJob(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.Configure<KafkaSetting>(configuration.GetSection(KafkaSetting.SettingKey));

        services.AddScoped<IIntegrationEventConsumer, IntegrationEventConsumer>();

        services.AddHostedService<KafkaEventConsumerHostedService>();

        return services;
    }
}