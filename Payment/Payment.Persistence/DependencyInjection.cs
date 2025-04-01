using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Payment.Persistence.Infrastructure;
using Payment.Persistence.Models;

namespace Payment.Persistence
{
    //Scaffold-DbContext "server=192.168.8.52;port=3306;user=vmdx;password=User1###;database=payment" Pomelo.EntityFrameworkCore.MySql -OutputDir Models -f -Context "PaymentDbContext"
    public static class DependencyInjection
    {
        public static IServiceCollection AddPaymentAppPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString(ConnectionString.SettingsKey)!;
            services.AddSingleton(new ConnectionString(connectionString));
            services.AddDbContext<PaymentDbContext>(options =>
            {
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            return services;
        }
    }
}
