using Domain.Core.Primitives.Result;

namespace Sale.Application.MonthlyReports.Commands.Updates.Confirm;

public sealed class ConfirmMonthlyReportCommand : ICommand<Result>
{
    public Guid MonthlyReportId { get; }

    public ConfirmMonthlyReportCommand(Guid monthlyReportId) => MonthlyReportId = monthlyReportId;
}