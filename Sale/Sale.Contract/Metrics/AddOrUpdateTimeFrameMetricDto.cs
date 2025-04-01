namespace Sale.Contract.Metrics;

/// <summary>
/// Represents a data transfer object for adding or updating time frame metrics.
/// </summary>
public class AddOrUpdateTimeFrameMetricDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the time frame.
    /// </summary>
    public Guid TimeFrameId { get; set; }

    /// <summary>
    /// Gets or sets the last stock number for the time frame.
    /// </summary>
    public double LastStockNumber { get; set; }

    /// <summary>
    /// Gets or sets the whole sales number for the time frame.
    /// </summary>
    public double WholeSalesNumber { get; set; }

    /// <summary>
    /// Gets or sets the retail sales number for the time frame.
    /// </summary>
    public double RetailSalesNumber { get; set; }

    /// <summary>
    /// Gets or sets the current stock number for the time frame.
    /// </summary>
    public double StockNumber { get; set; }
}
