using Sale.Contract.Products;
using Sale.Contract.TimeFrames;

namespace Sale.Contract.Metrics;

/// <summary>
/// Represents the response from the GetMetrics method.
/// </summary>
public class GetMetricsResponse
{
    /// <summary>
    /// The product associated with the metrics.
    /// </summary>
    public ProductResponse? Product { get; set; }

    /// <summary>
    /// A list of time frame metrics associated with the product.
    /// </summary>
    public List<TimeFrameMetricDto>? TimeFrameMetrics { get; set; }
}

/// <summary>
/// Represents a time frame metric associated with a product.
/// </summary>
public class TimeFrameMetricDto
{
    /// <summary>
    /// The unique identifier of the metric.
    /// </summary>
    public Guid MetricId { get; set; }

    /// <summary>
    /// The total number of orders for the time frame.
    /// </summary>
    public double OrderNumber { get; set; }

    /// <summary>
    /// The total number of returns for the time frame.
    /// </summary>
    public double ReturnNumber { get; set; }

    /// <summary>
    /// The current price of the product for the time frame.
    /// </summary>
    public double CurrentPrice { get; set; }

    /// <summary>
    /// A list of order IDs associated with the time frame.
    /// </summary>
    public List<int>? OrderIds { get; set; }

    /// <summary>
    /// A list of return IDs associated with the time frame.
    /// </summary>
    public List<int>? ReturnIds { get; set; }

    /// <summary>
    /// The forecast metric details for the time frame.
    /// </summary>
    public MetricDetailResponse? ForeCast { get; set; }

    /// <summary>
    /// The target metric details for the time frame.
    /// </summary>
    public MetricDetailResponse? Target { get; set; }

    /// <summary>
    /// The original budget metric details for the time frame.
    /// </summary>
    public MetricDetailResponse? OriginalBudget { get; set; }

    /// <summary>
    /// The time frame associated with the metric.
    /// </summary>
    public TimeFrameResponse? TimeFrame { get; set; }
}