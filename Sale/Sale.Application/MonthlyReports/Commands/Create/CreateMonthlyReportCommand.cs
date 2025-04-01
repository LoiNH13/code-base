using Domain.Core.Primitives.Result;
using Sale.Contract.MonthlyReportItems;
using Sale.Contract.MonthlyReports;
using Sale.Domain.Core.Abstractions;

namespace Sale.Application.MonthlyReports.Commands.Create;

public sealed class CreateMonthlyReportCommand : ICommand<Result<CreateMonthlyReportResponse>>
{
    public CreateMonthlyReportCommand(DateTime fromTimeOnUtc,
                                      DateTime? toTimeOnUtc,
                                      IDynamicValue dynamicValue,
                                      string? note,
                                      int odooCustomerId,
                                      List<CreateMonthlyReportItemRequest>? items)
    {
        FromTimeOnUtc = fromTimeOnUtc;
        ToTimeOnUtc = toTimeOnUtc;
        DynamicValue = dynamicValue;
        Note = note;
        OdooCustomerId = odooCustomerId;
        Items = items;
    }

    public DateTime FromTimeOnUtc { get; }

    public DateTime? ToTimeOnUtc { get; }

    public IDynamicValue DynamicValue { get; }

    public string? Note { get; }

    public int OdooCustomerId { get; }

    public List<CreateMonthlyReportItemRequest>? Items { get; }
}
