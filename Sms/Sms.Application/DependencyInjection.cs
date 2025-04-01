using Application.Core.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Sms.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddSmsApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

            cfg.AddOpenBehavior(typeof(LoggingPipelineBehavior<,>));

            cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));

            cfg.AddOpenBehavior(typeof(TransactionBehaviour<,>));
        });

        return services;
    }
}