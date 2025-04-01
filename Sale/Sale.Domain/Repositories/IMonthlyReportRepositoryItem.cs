using Sale.Domain.Entities.MonthlyReports;

namespace Sale.Domain.Repositories;

public interface IMonthlyReportRepositoryItem
{
    void Remove(MonthlyReportItem entity);
}
