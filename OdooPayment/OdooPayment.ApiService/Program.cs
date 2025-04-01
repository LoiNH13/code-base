using ApiService;
using ApiService.Middleware;
using Odoo.Infrastructure;
using Odoo.Persistence;
using OdooPayment.Application;
using OdooPayment.Background;
using Payment.Persistence;
using Persistence;
using Serilog;
using ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddServiceDefaults();
//builder.Services.AddAllElasticApm();

// Configure Serilog.
builder.Host.UseSerilog((context, configuration)
    => configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddApiService().AddPersistence()
    .AddPaymentAppPersistence(builder.Configuration)
    .AddOdooPersistence(builder.Configuration)
    .AddOdooInfrastructure(builder.Configuration)
    .AddOdooPaymentApplication()
    .AddOdooPaymentBackground(builder.Configuration)
    .AddOdooPaymentInfrastructure();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseSerilogRequestLogging();

app.UseCustomExceptionHandler();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
