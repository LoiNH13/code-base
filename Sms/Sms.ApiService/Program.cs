using ApiService;
using ApiService.Middleware;
using Infrastructure;
using Persistence;
using Serilog;
using ServiceDefaults;
using Sms.Application;
using Sms.Background;
using Sms.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();
builder.Services.AddAllElasticApm();

//configure the custom services.
builder.Services.AddApiService()
    .AddPersistence()
    .AddInfrastructure(builder.Configuration)
    .AddSmsApplication()
    .AddSmsBackgroundJob(builder.Configuration)
    .AddSmsPersistence(builder.Configuration);

// Configure Serilog.
builder.Host.UseSerilog((context, configuration)
    => configuration.ReadFrom.Configuration(context.Configuration));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

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

app.Services.RunMigrations<SmsDbContext>();

await app.RunAsync();