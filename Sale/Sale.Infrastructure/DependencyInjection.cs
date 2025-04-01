using Application.Core.Authentication;
using Infrastructure;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Sale.Application.Core.Authentication;
using Sale.Infrastructure.Authentication;
using Sale.Infrastructure.Authentication.Settings;
using System.Text;

namespace Sale.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddSaleInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);

        string jwtSecurityKey = configuration["Jwt:SecurityKey"]!;
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecurityKey))
        });

        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SettingsKey));

        services.AddScoped<IUserIdentifierProvider, UserIdentifierProvider>();

        services.AddScoped<IJwtProvider, JwtProvider>();

        services.AddTransient<IVietmapAuthenticationService, VietmapAuthenticationService>();

        return services;
    }
}
