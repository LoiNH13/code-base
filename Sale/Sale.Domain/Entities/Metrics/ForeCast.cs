using Domain.Core.Abstractions;
using Domain.Core.Primitives;
using Sale.Domain.Core.Abstractions;

namespace Sale.Domain.Entities.Metrics;

public sealed class ForeCast : Entity, IMetricAbstract, IAuditableEntity
{
    internal ForeCast(Metric metric, double price, double lastStockNumber, double wholeSalesNumber, double retailSalesNumber, double stockNumber)
    {
        MetricId = metric.Id;
        Price = price;
        LastStockNumber = lastStockNumber;
        WholeSalesNumber = wholeSalesNumber;
        RetailSalesNumber = retailSalesNumber;
        StockNumber = stockNumber;
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

    private ForeCast() { }

    internal void Update(double price, double lastStockNumber, double wholeSalesNumber, double retailSalesNumber, double stockNumber)
    {
        Price = price;
        LastStockNumber = lastStockNumber;
        WholeSalesNumber = wholeSalesNumber;
        RetailSalesNumber = retailSalesNumber;
        StockNumber = stockNumber;
    }

    internal void UpdateWholeSales(double wholeSalesNumber) => WholeSalesNumber = wholeSalesNumber;

    internal void UpdateLastStock(double lastStockNumber) => LastStockNumber = lastStockNumber;

    internal void ReCalculateStock(double returnOrderNumber)
        => StockNumber = LastStockNumber + WholeSalesNumber - RetailSalesNumber - returnOrderNumber;

    internal void Clear()
    {
        LastStockNumber = 0;
        WholeSalesNumber = 0;
        RetailSalesNumber = 0;
        StockNumber = 0;
    }
}
