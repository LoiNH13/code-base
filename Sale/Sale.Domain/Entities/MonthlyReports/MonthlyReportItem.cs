using Domain.Core.Abstractions;
using Domain.Core.Primitives;
using Domain.Core.Primitives.Result;
using Sale.Domain.Enumerations;

namespace Sale.Domain.Entities.MonthlyReports;

public sealed class MonthlyReportItem : Entity, IAuditableEntity
{
    internal MonthlyReportItem(Guid monthlyReportId,
                               Guid categoryId,
                               double quantity,
                               double revenue,
                               EMonthlyReportItem group,
                               string? note)
    {
        MonthlyReportId = monthlyReportId;
        CategoryId = categoryId;
        Quantity = quantity;
        Revenue = revenue;
        Note = note;
        Group = group;
    }

    public Guid MonthlyReportId { get; init; }

    public Guid CategoryId { get; init; }

    public EMonthlyReportItem Group { get; init; } = EMonthlyReportItem.Default;

    public double Quantity { get; private set; }

    public double Revenue { get; private set; }

    public string? Note { get; private set; }

    public DateTime CreatedOnUtc { get; }

    public DateTime? ModifiedOnUtc { get; }

    private MonthlyReportItem()
    {
    }

    internal Result Update(double quantity, double revenue, string? note)
    {
        Quantity = quantity;
        Revenue = revenue;
        Note = note;
        return Result.Success();
    }
}
