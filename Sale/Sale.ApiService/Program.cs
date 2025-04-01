global using ApiService.Contracts;
global using ApiService.Infrastructure;
global using MediatR;
using ApiService;
using ApiService.Extensions;
using ApiService.Middleware;
using Background;
using Microsoft.OpenApi.Models;
using Odoo.Persistence;
using Persistence;
using Sale.Application;
using Sale.Background;
using Sale.Infrastructure;
using Sale.Persistence;
using Serilog;
using ServiceDefaults;
using System.Reflection;

var appName = Assembly.GetExecutingAssembly().GetName().Name;
var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// Configure Serilog.
builder.Host.UseSerilog((context, configuration)
    => configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddAllElasticApm();

// Configure application services.
builder.Services.AddApiService()
    .AddSaleApplication()
    .AddPersistence()
    .AddSalePersistence(builder.Configuration)
    .AddSaleInfrastructure(builder.Configuration)
    .AddSaleBackgroundJob(builder.Configuration)
    .AddOdooPersistence(builder.Configuration);

// Configure Swagger.
builder.Services.AddSwaggerGen(swaggerGenOptions =>
{
    swaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Salesforce API",
        Version = "v1"
    });

    // Lấy comment từ file XML (nếu có)
    swaggerGenOptions.IncludeXmlCommentsFromAllProjects();

    swaggerGenOptions.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    swaggerGenOptions.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();

app.UseSwagger();
app.UseSwaggerUI(swaggerUiOptions =>
{
    swaggerUiOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "Saleforce API");
    swaggerUiOptions.DisplayRequestDuration();
});

// Configure HTTPS.
app.UseHttpsRedirection();

// Configure CORS.
app.UseCors(b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.UseCustomExceptionHandler();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapDefaultEndpoints();

app.Services.RunMigrations<SaleDbContext>();

app.ApplyHangfire(appName ?? "Hangfire", "admin", "admin");

await app.RunAsync();