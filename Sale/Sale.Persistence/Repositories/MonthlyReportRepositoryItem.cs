using Application.Core.Abstractions.Data;
using Persistence.Repositories;
using Sale.Domain.Entities.MonthlyReports;
using Sale.Domain.Repositories;

namespace Sale.Persistence.Repositories;

internal sealed class MonthlyReportRepositoryItem(IDbContext dbContext) :
    GenericRepository<MonthlyReportItem>(dbContext),
    IMonthlyReportRepositoryItem
{
}
