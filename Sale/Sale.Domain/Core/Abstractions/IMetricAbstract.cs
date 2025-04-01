namespace Sale.Domain.Core.Abstractions;

public interface IMetricAbstract
{
    Guid MetricId { get; }

    double Price { get; }

    double LastStockNumber { get; }

    double WholeSalesNumber { get; }

    double RetailSalesNumber { get; }

    double StockNumber { get; }
}