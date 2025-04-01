using Domain.Core.Primitives;
using Newtonsoft.Json;
using Sale.Domain.Core.Abstractions;
using Sale.Domain.Enumerations;

namespace Sale.Domain.ValueObjects;

public sealed class DealerMonthlyReport : ValueObject, IDynamicValue
{
    public DealerMonthlyReport(int dailyVisitors, int dailyPurchases, double onlinePurchaseRate)
    {
        DailyVisitors = dailyVisitors;
        DailyPurchases = dailyPurchases;
        OnlinePurchaseRate = onlinePurchaseRate;
    }

    public int DailyVisitors { get; internal set; }

    public int DailyPurchases { get; internal set; }

    public double OnlinePurchaseRate { get; internal set; }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return DailyVisitors;
        yield return DailyPurchases;
        yield return OnlinePurchaseRate;
    }

    public string Serialize() =>
        JsonConvert.SerializeObject(this);

    public EBusinessType GetBusinessType() => EBusinessType.Dealer;

    private DealerMonthlyReport()
    {
    }
}
