using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Odoo.Persistence.Infrastructure;

namespace Odoo.Persistence.Tasks;

internal sealed class LoadOdooSchemaBackgroundService(
    ILogger<LoadOdooSchemaBackgroundService> logger,
    IServiceProvider serviceProvider)
    : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogDebug("LoadOdooSchemaBackgroundService is starting.");

        var state = (this, stoppingToken);
        var timer = new Timer(TimerCallback!, state, TimeSpan.FromSeconds(1), Timeout.InfiniteTimeSpan);

        // Ensure the timer is disposed when the service stops
        stoppingToken.Register(() =>
        {
            timer.Dispose();
            logger.LogDebug("LoadOdooSchemaBackgroundService timer disposed.");
        });

        logger.LogDebug("LoadOdooSchemaBackgroundService has scheduled the database check.");

        return Task.CompletedTask;

        // Use a static Timer to avoid allocations on each service start
        static void TimerCallback(object state)
        {
            var (service, token) = ((LoadOdooSchemaBackgroundService, CancellationToken))state;
            service.CheckDatabaseConnection(token);
        }
    }

    private void CheckDatabaseConnection(CancellationToken stoppingToken)
    {
        try
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<OdooDbContext>();

            logger.LogDebug("LoadOdooSchemaBackgroundService is attempting to connect to the database.");

            // Use a value task to avoid unnecessary allocations
            var canConnectTask = dbContext.Database.CanConnectAsync(stoppingToken);

            // Use GetAwaiter().GetResult() to avoid allocating a Task
            var canConnect = canConnectTask.IsCompleted
                ? canConnectTask.Result
                : canConnectTask.GetAwaiter().GetResult();

            if (canConnect)
                logger.LogInformation("Successfully connected to the Odoo database.");
            else
                logger.LogWarning("Failed to connect to the Odoo database.");
        }
        catch (OperationCanceledException ex)
        {
            logger.LogInformation(ex, "Database connection attempt was cancelled.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while checking the Odoo database connection.");
        }
    }
}