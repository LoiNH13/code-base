using Domain.Core.Primitives.Result;
using Sale.Contract.MonthlyReportItems;

namespace Sale.Application.MonthlyReportItems.Commands.Updates.ListUpdate;

public sealed class ListUpdateMonthlyReportItemCommand : ICommand<Result>
{
    public Guid MonthlyReportId { get; }

    public List<UpdateMonthlyReportItem2Request> Items { get; }

    public ListUpdateMonthlyReportItemCommand(Guid monthlyReportId, List<UpdateMonthlyReportItem2Request> items)
    {
        MonthlyReportId = monthlyReportId;
        Items = items;
    }
}
