using Integration.Kafka.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OdooPayment.Background.Contracts.Settings;
using OdooPayment.Background.Tasks;

namespace OdooPayment.Background
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddOdooPaymentBackground(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<KafkaSetting>(configuration.GetSection(KafkaSetting.SettingKey));
            services.Configure<BankAcountSetting>(configuration.GetSection(BankAcountSetting.SettingKey));

            services.AddHostedService<CreditAdviceHostedService>();

            return services;
        }
    }
}
