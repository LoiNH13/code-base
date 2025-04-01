using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace ApiService;

public static class DependencyInjection
{
    public static IServiceCollection AddApiService(this IServiceCollection services)
    {
        // Add services to the container.
        services.AddProblemDetails();

        // Configure controllers.
        services.AddControllers()
            .AddJsonOptions(option =>
            {
                option.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                option.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

        // Configure FluentValidation.
        services.AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters();

        // Configure MVC.
        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

        return services;
    }
}