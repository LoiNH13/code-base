using Background.Settings;
using Hangfire;
using Hangfire.Redis.StackExchange;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Background;

public static class DependencyInjection
{
    public static IServiceCollection AddBackgroundJob(this IServiceCollection services, IConfiguration configuration, string prefix = "")
    {
        services.AddHangfire(config => config
             .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
             .UseSimpleAssemblyNameTypeSerializer()
             .UseRecommendedSerializerSettings()
             .UseRedisStorage($"{configuration["Redis:Hangfire:ConnectionString"]},password={configuration["Redis:Hangfire:Password"]}"
                 , new RedisStorageOptions()
                 {
                     Db = int.Parse(configuration["Redis:Hangfire:Database"] ?? "0"),
                     Prefix = prefix,
                 }
             )
        );

        services.AddHangfireServer(options =>
        {
            options.Queues = [BackgroundJobConst.Queues.Alpha, BackgroundJobConst.Queues.Default];
            options.WorkerCount = 20;
        });

        return services;
    }
}
