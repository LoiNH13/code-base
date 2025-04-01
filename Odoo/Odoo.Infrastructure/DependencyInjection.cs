using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Odoo.Application.Core.Odoo;
using Odoo.Infrastructure.Odoo;
using Odoo.Infrastructure.Odoo.Settings;

namespace Odoo.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddOdooInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<OdooServicesSetting>(configuration.GetSection(OdooServicesSetting.SettingKey));

            services.AddScoped<IOdooServices, OdooServices>();

            return services;
        }
    }
}
