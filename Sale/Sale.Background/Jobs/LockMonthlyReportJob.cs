using Application.Core.Abstractions.Common;
using Application.Core.Abstractions.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sale.Domain.Repositories;

namespace Sale.Background.Jobs;

internal sealed class LockMonthlyReportJob(
    IMonthlyReportRepository monthlyReportRepository,
    IUnitOfWork unitOfWork,
    ILogger<LockMonthlyReportJob> logger,
    IDateTime dateTime)
{
    public async Task Run()
    {
        await using var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            var monthlyReports = await monthlyReportRepository.Queryable()
                .Where(x => x.FromTimeOnUtc.Year * 12 + x.FromTimeOnUtc.Month == dateTime.CurrentConvertMonths - 1)
                .ToListAsync();

            if (monthlyReports.Count > 0)
            {
                foreach (var report in monthlyReports) report.Confirm();

                await unitOfWork.SaveChangesAsync();
                await transaction.CommitAsync();
                logger.LogInformation("Successfully locked {Count} monthly reports.", monthlyReports.Count);
            }
            else
            {
                logger.LogInformation("No monthly reports found to lock.");
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            logger.LogError(ex, "Failed to lock monthly reports: {ErrorMessage}.", ex.Message);
        }
    }
}