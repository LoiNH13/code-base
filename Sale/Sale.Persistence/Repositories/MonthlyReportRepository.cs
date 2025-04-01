using Application.Core.Abstractions.Data;
using Domain.Core.Primitives.Maybe;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;
using Sale.Domain.Entities.MonthlyReports;
using Sale.Domain.Repositories;

namespace Sale.Persistence.Repositories;

internal sealed class MonthlyReportRepository(IDbContext dbContext) : GenericRepository<MonthlyReport>(dbContext), IMonthlyReportRepository
{
    public override async Task<Maybe<MonthlyReport>> GetByIdAsync(Guid id) =>
        await DbContext.Set<MonthlyReport>()
        .Include(x => x.Items)
        .FirstOrDefaultAsync(x => x.Id == id) ?? default!;
}
