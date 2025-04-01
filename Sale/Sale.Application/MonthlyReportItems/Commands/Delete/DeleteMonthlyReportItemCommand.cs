using Domain.Core.Primitives.Result;

namespace Sale.Application.MonthlyReportItems.Commands.Delete;

public sealed class DeleteMonthlyReportItemCommand : ICommand<Result>
{
    public DeleteMonthlyReportItemCommand(Guid monthlyReportId, Guid monthlyReportItemId)
    {
        MonthlyReportId = monthlyReportId;
        MonthlyReportItemId = monthlyReportItemId;
    }

    public Guid MonthlyReportId { get; }

    public Guid MonthlyReportItemId { get; }
}
