using Application.Core.Abstractions.Common;
using Application.Core.Abstractions.Cryptography;
using Application.Core.Emails;
using Domain.Services;
using Infrastructure.Common;
using Infrastructure.Cryptography;
using Infrastructure.Emails;
using Infrastructure.Emails.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MailSettings>(configuration.GetSection(MailSettings.SettingsKey));

        services.AddTransient<IEmailService, EmailService>();

        services.AddTransient<IDateTime, MachineDateTime>();

        services.AddTransient<IPasswordHasher, PasswordHasher>();

        services.AddTransient<IPasswordHashChecker, PasswordHasher>();

        return services;
    }
}
