using Domain.Core.Primitives.Result;
using Sale.Domain.Enumerations;

namespace Sale.Application.MonthlyReportItems.Commands.Create;

public sealed class CreateMonthlyReportItemCommand : ICommand<Result>
{
    public CreateMonthlyReportItemCommand(Guid monthlyReportId,
                                          Guid categoryId,
                                          EMonthlyReportItem? group,
                                          double quantity,
                                          double revenue,
                                          string? note)
    {
        MonthlyReportId = monthlyReportId;
        CategoryId = categoryId;
        Quantity = quantity;
        Revenue = revenue;
        Note = note;
    }

    public Guid MonthlyReportId { get; }

    public Guid CategoryId { get; }

    public double Quantity { get; }

    public double Revenue { get; }

    public EMonthlyReportItem Group { get; } = EMonthlyReportItem.Default;

    public string? Note { get; }
}
