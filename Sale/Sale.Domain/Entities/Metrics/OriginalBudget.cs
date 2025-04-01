using Domain.Core.Abstractions;
using Domain.Core.Primitives;
using Sale.Domain.Core.Abstractions;

namespace Sale.Domain.Entities.Metrics;

public sealed class OriginalBudget : Entity, IMetricAbstract, IAuditableEntity
{
    internal OriginalBudget(Metric metric, double price, int lastStockNumber, int wholeSalesNumber, int retailSalesNumber, int stockNumber)
    {
        MetricId = metric.Id;
        Price = price;
        LastStockNumber = lastStockNumber;
        WholeSalesNumber = wholeSalesNumber;
        RetailSalesNumber = retailSalesNumber;
        StockNumber = stockNumber;
    }

    internal OriginalBudget(IMetricAbstract data)
    {
        MetricId = data.MetricId;
        Price = data.Price;
        LastStockNumber = data.LastStockNumber;
        WholeSalesNumber = data.WholeSalesNumber;
        RetailSalesNumber = data.RetailSalesNumber;
        StockNumber = data.StockNumber;
    }

    public Guid MetricId { get; init; }

    public double Price { get; private set; }

    public double LastStockNumber { get; private set; }

    public double WholeSalesNumber { get; private set; }

    public double RetailSalesNumber { get; private set; }

    public double StockNumber { get; private set; }

    public DateTime CreatedOnUtc { get; }

    public DateTime? ModifiedOnUtc { get; }

    public double TotalAmount => Price * WholeSalesNumber;

    private OriginalBudget() { }

    internal void Update(IMetricAbstract data)
    {
        Price = data.Price;
        LastStockNumber = data.LastStockNumber;
        WholeSalesNumber = data.WholeSalesNumber;
        RetailSalesNumber = data.RetailSalesNumber;
        StockNumber = data.StockNumber;
    }
}
