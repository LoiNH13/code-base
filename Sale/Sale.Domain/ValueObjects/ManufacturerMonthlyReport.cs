using Domain.Core.Primitives;
using Newtonsoft.Json;
using Sale.Domain.Core.Abstractions;
using Sale.Domain.Enumerations;

namespace Sale.Domain.ValueObjects;

public sealed class ManufacturerMonthlyReport : ValueObject, IDynamicValue
{
    public int NewBuyInMonth { get; set; }

    public int NewBuyNextMonth { get; set; }

    public double ServiceInMonth { get; set; }

    public EBusinessType GetBusinessType() => EBusinessType.Manufacturer;

    public string Serialize() => JsonConvert.SerializeObject(this);

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return NewBuyInMonth;
        yield return NewBuyNextMonth;
        yield return ServiceInMonth;
    }

    private ManufacturerMonthlyReport()
    {
    }

    public ManufacturerMonthlyReport(int newBuyInMonth, int newBuyNextMonth, double serviceInMonth)
    {
        NewBuyInMonth = newBuyInMonth;
        NewBuyNextMonth = newBuyNextMonth;
        ServiceInMonth = serviceInMonth;
    }
}
