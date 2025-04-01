
using Sale.Contract.TimeFrames;
using Sale.Domain.Enumerations;

namespace Sale.Contract.Metrics;

/// <summary>
/// Represents a request for generating a summary report by customer.
/// </summary>
public class SummaryByCustomerRequest
{
    /// <summary>
    /// Gets or sets the start month and year for the report.
    /// </summary>
    /// <remarks>Defaults to the current month and year.</remarks>
    public MonthYearDto FromMY { get; set; } = new();

    /// <summary>
    /// Gets or sets the end month and year for the report.
    /// </summary>
    /// <remarks>Defaults to the current month and year.</remarks>
    public MonthYearDto ToMY { get; set; } = new();

    /// <summary>
    /// Gets or sets the type of metric to be included in the report.
    /// </summary>
    public EMetricType MetricType { get; set; }
}
