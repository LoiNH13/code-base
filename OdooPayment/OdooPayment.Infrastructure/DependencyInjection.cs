using Microsoft.Extensions.DependencyInjection;
using OdooPayment.Application.Core.Odoo;
using OdooPayment.Infrastructure.Odoo;

namespace OdooPayment.Background
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddOdooPaymentInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IOdooIntegrationSerivce, OdooIntegrationService>();

            return services;
        }
    }
}
