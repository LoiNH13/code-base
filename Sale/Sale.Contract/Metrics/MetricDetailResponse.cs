using Sale.Domain.Core.Abstractions;

namespace Sale.Contract.Metrics;

/// <summary>
/// Represents a response for metric details.
/// </summary>
public class MetricDetailResponse
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MetricDetailResponse"/> class.
    /// </summary>
    /// <param name="data">The metric data to initialize the response with.</param>
    public MetricDetailResponse(IMetricAbstract data)
    {
        Price = data.Price;
        LastStockNumber = data.LastStockNumber;
        WholeSalesNumber = data.WholeSalesNumber;
        RetailSalesNumber = data.RetailSalesNumber;
        StockNumber = data.StockNumber;
    }

    /// <summary>
    /// Gets or sets the price of the metric.
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// Gets or sets the last stock number of the metric.
    /// </summary>
    public double LastStockNumber { get; set; }

    /// <summary>
    /// Gets or sets the whole sales number of the metric.
    /// </summary>
    public double WholeSalesNumber { get; set; }

    /// <summary>
    /// Gets or sets the retail sales number of the metric.
    /// </summary>
    public double RetailSalesNumber { get; set; }

    /// <summary>
    /// Gets or sets the current stock number of the metric.
    /// </summary>
    public double StockNumber { get; set; }
}