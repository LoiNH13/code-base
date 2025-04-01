using Domain.Core.Primitives.Result;
using Sale.Contract.MonthlyReportItems;
using Sale.Domain.Core.Abstractions;

namespace Sale.Application.MonthlyReports.Commands.Updates.Update;

public sealed class UpdateMonthlyReportCommand : ICommand<Result>
{
    public UpdateMonthlyReportCommand(Guid monthlyReportId,
                                      DateTime fromTimeOnUtc,
                                      DateTime? toTimeOnUtc,
                                      IDynamicValue dynamicData,
                                      string? note,
                                      List<UpdateMonthlyReportItem2Request>? items)
    {
        MonthlyReportId = monthlyReportId;
        FromTimeOnUtc = fromTimeOnUtc;
        ToTimeOnUtc = toTimeOnUtc;
        DynamicData = dynamicData;
        Note = note;
        Items = items ?? new();
    }

    public Guid MonthlyReportId { get; }

    public DateTime FromTimeOnUtc { get; }

    public DateTime? ToTimeOnUtc { get; }

    public int DailyVisitors { get; }

    public int DailyPurchases { get; }

    public double OnlinePurchaseRate { get; }

    public string? Note { get; }

    public IDynamicValue DynamicData { get; }

    public List<UpdateMonthlyReportItem2Request> Items { get; }
}
