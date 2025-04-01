using Domain.Core.Primitives.Maybe;
using Sale.Domain.Entities.MonthlyReports;

namespace Sale.Domain.Repositories;

public interface IMonthlyReportRepository
{
    IQueryable<MonthlyReport> Queryable();

    void Insert(MonthlyReport entity);

    Task<Maybe<MonthlyReport>> GetByIdAsync(Guid id);
}
