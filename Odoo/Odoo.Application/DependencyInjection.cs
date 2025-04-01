using Application.Core.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Odoo.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddOdooApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

                cfg.AddOpenBehavior(typeof(LoggingPipelineBehavior<,>));

                cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            });
            return services;
        }
    }
}
