using Domain.Core.Primitives.Result;

namespace Sale.Application.MonthlyReportItems.Commands.Updates.Update;

public sealed class UpdateMonthlyReportItemCommand : ICommand<Result>
{
    public UpdateMonthlyReportItemCommand(Guid monthlyReportId, Guid monthlyReportItemId, double quantity, double revenue, string? note)
    {
        MonthlyReportId = monthlyReportId;
        MonthlyReportItemId = monthlyReportItemId;
        Quantity = quantity;
        Revenue = revenue;
        Note = note;
    }

    public Guid MonthlyReportId { get; }

    public Guid MonthlyReportItemId { get; }

    public double Quantity { get; }

    public double Revenue { get; }

    public string? Note { get; }
}
