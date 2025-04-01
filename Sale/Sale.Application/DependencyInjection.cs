global using Application.Core.Abstractions.Messaging;
using Application.Core.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Sale.Application.Core.Behaviors;
using System.Reflection;

namespace Sale.Application;

public static class DependencyInjection
{
    /// <summary>
    /// Registers the necessary services with the DI framework.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The same service collection.</returns>
    public static IServiceCollection AddSaleApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

            cfg.AddOpenBehavior(typeof(LoggingPipelineBehavior<,>));

            cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));

            cfg.AddOpenBehavior(typeof(TransactionBehaviour<,>));

            cfg.AddOpenBehavior(typeof(ManageUsersBehavior<,>));
        });

        return services;
    }
}
