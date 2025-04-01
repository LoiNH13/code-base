using Infrastructure;
using Integration.Abstractions;
using Integration.Kafka.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MOT.Application.Core.Abstractions.DxSms;
using MOT.Application.Core.Abstractions.VietmapLive;
using MOT.Contract.Core.FTI;
using MOT.Infrastructure.DxSms;
using MOT.Infrastructure.Kafka;
using MOT.Infrastructure.VietmapLive;
using MOT.Infrastructure.VietmapLive.Settings;

namespace MOT.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddMotInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);

        services.Configure<KafkaSetting>(configuration.GetSection(KafkaSetting.SettingKey));

        services.Configure<FtiSetting>(configuration.GetSection(FtiSetting.SettingKey));

        services.Configure<VietmapLiveSetting>(configuration.GetSection(VietmapLiveSetting.SettingKey));

        services.AddSingleton<IKafkaEventPublisher, KafkaEventPublisher>();

        services.AddTransient<IVietmapLiveService, VietmapLiveService>();

        services.AddTransient<ISmsService, SmsService>();

        return services;
    }
}