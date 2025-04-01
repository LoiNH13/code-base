using Application.Core.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace OdooPayment.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddOdooPaymentApplication(this IServiceCollection services)
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
