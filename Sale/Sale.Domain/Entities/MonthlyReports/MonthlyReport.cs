using Domain.Core.Abstractions;
using Domain.Core.Primitives;
using Domain.Core.Primitives.Result;
using Newtonsoft.Json;
using Sale.Domain.Core.Abstractions;
using Sale.Domain.Core.Errors;
using Sale.Domain.Entities.Customers;
using Sale.Domain.Entities.Products;
using Sale.Domain.Enumerations;
using Sale.Domain.Validators;
using Sale.Domain.ValueObjects;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace Sale.Domain.Entities.MonthlyReports;

public sealed class MonthlyReport : AggregateRoot, IAuditableEntity, IMustHaveCreateUser, ISoftDeletableEntity
{
    private string _dynamicData;

    public MonthlyReport(Customer customer,
                         DateTime fromTimeOnUtc,
                         DateTime? toTimeOnUtc,
                         IDynamicValue dynamicValue,
                         string? note) : base(Guid.NewGuid())
    {
        CustomerId = customer.Id;
        FromTimeOnUtc = fromTimeOnUtc;
        ToTimeOnUtc = toTimeOnUtc ?? fromTimeOnUtc.AddDays(1).Date.AddHours(-1);
        IsConfirmed = false;
        Note = note;
        BusinessType = dynamicValue.GetBusinessType();
        _dynamicData = dynamicValue.Serialize();
    }

    public Guid CustomerId { get; init; }

    public EBusinessType BusinessType { get; private set; }

    public DateTime FromTimeOnUtc { get; private set; }

    public DateTime ToTimeOnUtc { get; private set; }

    public int DailyVisitors { get; }

    public int DailyPurchases { get; }

    public double OnlinePurchaseRate { get; }

    public DealerMonthlyReport? DealerMonthlyReport => _dynamicData.Equals("{}") ? null :
        JsonConvert.DeserializeObject<DealerMonthlyReport>(_dynamicData);

    public ManufacturerMonthlyReport? ManufacturerMonthlyReport => _dynamicData.Equals("{}") ? null :
        JsonConvert.DeserializeObject<ManufacturerMonthlyReport>(_dynamicData);

    public string? Note { get; private set; }

    public bool IsConfirmed { get; private set; }

    public DateTime CreatedOnUtc { get; }

    public Guid CreateByUser { get; init; }

    public DateTime? ModifiedOnUtc { get; }

    public DateTime? DeletedOnUtc { get; }

    public bool Deleted { get; }

    public List<MonthlyReportItem> Items { get; private set; } = new List<MonthlyReportItem>();

    private MonthlyReport()
    {
    }

    public Result SetDynamicData(IDynamicValue data)
    {
        if (data.GetBusinessType() != BusinessType)
            return Result.Failure(SaleDomainErrors.MonthlyReport.BusinessTypeMismatch.AddParams(BusinessType, data.GetBusinessType()));
        _dynamicData = data.Serialize();
        return Result.Success();
    }

    public Result Update(DateTime fromTimeOnUtc,
                         DateTime? toTimeOnUtc,
                         IDynamicValue dynamicValue,
                         string? note)
    {
        if (IsConfirmed) return Result.Failure(SaleDomainErrors.MonthlyReport.ReportConfirmed);

        FromTimeOnUtc = fromTimeOnUtc;
        ToTimeOnUtc = toTimeOnUtc ?? fromTimeOnUtc.AddDays(1).Date.AddHours(-1);
        if (this.CheckFromToValid(out Result result).IsFailure) return result;

        result = SetDynamicData(dynamicValue);
        if (result.IsFailure) return result;

        Note = note;
        return result;
    }

    public Result Confirm()
    {
        if (this.CheckFromToValid(out Result result).IsFailure) return result;
        IsConfirmed = true;
        return result;
    }

    public Result AddItem(Category category, double quantity, double revenue, string? note, EMonthlyReportItem group)
    {
        if (IsConfirmed) return Result.Failure(SaleDomainErrors.MonthlyReport.ReportConfirmed);
        if (Items.Exists(x => x.CategoryId == category.Id && x.Group == group))
            return Result.Failure(SaleDomainErrors.MonthlyReportItem.CategoryAlreadyExists.AddParams(category.Name));

        Items.Add(new MonthlyReportItem(Id, category.Id, quantity, revenue, group, note));
        return Result.Success();
    }

    public Result UpdateItem(Guid itemId, double quantity, double revenue, string? note)
    {
        if (IsConfirmed) return Result.Failure(SaleDomainErrors.MonthlyReport.ReportConfirmed);
        MonthlyReportItem item = Items.Find(i => i.Id == itemId)!;
        if (item is null) return Result.Failure(SaleDomainErrors.MonthlyReportItem.NotFound.AddParams(itemId));

        item.Update(quantity, revenue, note);
        return Result.Success();
    }

    public Result RemoveItem(Guid itemId)
    {
        if (IsConfirmed) return Result.Failure(SaleDomainErrors.MonthlyReport.ReportConfirmed);

        MonthlyReportItem item = Items.Find(i => i.Id == itemId)!;
        if (item is null) return Result.Failure(SaleDomainErrors.MonthlyReportItem.NotFound.AddParams(itemId));

        Items.Remove(item);
        return Result.Success();
    }
}
