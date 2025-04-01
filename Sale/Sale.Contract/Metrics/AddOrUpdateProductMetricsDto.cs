namespace Sale.Contract.Metrics;

/// <summary>
/// Represents a data transfer object for adding or updating product metrics.
/// </summary>
public class AddOrUpdateProductMetricsDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the product.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets a list of time frame metrics associated with the product.
    /// This property is optional and can be null if no time frame metrics are provided.
    /// </summary>
    public List<AddOrUpdateTimeFrameMetricDto>? TimeFrameMetrics { get; set; }
}
