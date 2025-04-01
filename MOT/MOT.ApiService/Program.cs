using ApiService;
using ApiService.Extensions;
using ApiService.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;
using MOT.ApiService.SOAP;
using MOT.Application;
using MOT.Infrastructure;
using Serilog;
using ServiceDefaults;
using SoapCore;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();
builder.Services.AddAllElasticApm();

// Configure Serilog.
builder.Host.UseSerilog((context, configuration)
    => configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddApiService()
    .AddMotApplication()
    .AddMotInfrastructure(builder.Configuration);

builder.Services.AddSoapCore();
builder.Services.AddScoped<IMoSoapServices, MoSoapServices>();

builder.Services.AddSwaggerGen(swaggerGenOptions =>
{
    swaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MOT API",
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

if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();

app.UseSwagger();
app.UseSwaggerUI(swaggerUiOptions =>
{
    swaggerUiOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "MOT API");
    swaggerUiOptions.DisplayRequestDuration();
});

app.UseHttpsRedirection();

app.UseCors(b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

app.UseCustomExceptionHandler();

app.UseRouting();

#pragma warning disable ASP0014
app.UseEndpoints(endpoints =>
{
    endpoints.UseSoapEndpoint<IMoSoapServices>("/soap/MoService.asmx", new SoapEncoderOptions(),
        SoapSerializer.XmlSerializer);
});
#pragma warning restore ASP0014

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapDefaultEndpoints();

await app.RunAsync();