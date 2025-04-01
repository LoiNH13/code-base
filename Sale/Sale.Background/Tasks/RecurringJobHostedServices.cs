using Background.Settings;
using Hangfire;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sale.Background.Jobs;
using Sale.Background.Jobs.Settings;

namespace Sale.Background.Tasks;

public sealed class RecurringJobHostedServices(ILogger<RecurringJobHostedServices> logger) : BackgroundService
{
    /// <summary>
    ///     Executes the recurring job hosted services, setting up scheduled tasks for customer synchronization and monthly
    ///     report locking.
    /// </summary>
    /// <param name="stoppingToken">A <see cref="CancellationToken" /> that can be used to stop the background service.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation. The task completes when the service stops.</returns>
    /// <remarks>
    ///     This method sets up the following recurring jobs:
    ///     <list type="bullet">
    ///         <item>Customer synchronization with Odoo, running every 4 hours.</item>
    ///         <item>Monthly customer synchronization with Odoo, running on the first day of each month.</item>
    ///         <item>Monthly report locking, running on the first day of each month.</item>
    ///     </list>
    /// </remarks>
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogDebug("RecurringJobHostedServices is starting.");

        stoppingToken.Register(() => logger.LogDebug("RecurringJobHostedServices background task is stopping."));

        RecurringJob.AddOrUpdate<ScheduleCustomerSyncOdoo>(
            RecurringJobs.OdooHostedServices.ScheduleCustomerSyncOdooRun,
            BackgroundJobConst.Queues.Default,
            x => x.Run(),
            "0 0,4,8,12,16,20 * * *",
            new RecurringJobOptions
            {
                TimeZone = TimeZoneInfo.Local
            });

        RecurringJob.AddOrUpdate<CategoriesSyncOdoo>(
            RecurringJobs.OdooHostedServices.CategoriesSyncOdoo,
            BackgroundJobConst.Queues.Default,
            x => x.Run(),
            "30 3,7,11,15,19,23 * * *",
            new RecurringJobOptions
            {
                TimeZone = TimeZoneInfo.Local
            });

        RecurringJob.AddOrUpdate<ProductsSyncOdoo>(
            RecurringJobs.OdooHostedServices.ProductsSyncOdoo,
            BackgroundJobConst.Queues.Default,
            x => x.Run(),
            "0 3,7,11,15,19,23 * * *",
            new RecurringJobOptions
            {
                TimeZone = TimeZoneInfo.Local
            });

        RecurringJob.AddOrUpdate<ScheduleCustomerSyncOdoo>(
            RecurringJobs.OdooHostedServices.ScheduleCustomerSyncOdooRunWithLastMonth,
            BackgroundJobConst.Queues.Default,
            x => x.RunWithLastMonth(),
            "0 1 1 * *");

        RecurringJob.AddOrUpdate<LockMonthlyReportJob>(
            RecurringJobs.MonthlyReportServices.LockMonthlyReport,
            BackgroundJobConst.Queues.Default,
            x => x.Run(),
            "0 1 1 * *");

        logger.LogDebug("RecurringJobHostedServices background task is stopping.");

        return Task.CompletedTask;
    }
}