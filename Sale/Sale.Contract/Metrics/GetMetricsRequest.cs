using Sale.Contract.TimeFrames;

namespace Sale.Contract.Metrics;

/// <summary>
/// Represents a request to retrieve metrics data.
/// </summary>
public class GetMetricsRequest
{
    /// <summary>
    /// Gets or sets the page number for pagination.
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// Gets or sets the page size for pagination.
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Gets or sets the start month and year for the metrics data.
    /// </summary>
    public MonthYearDto FromMY { get; set; } = new();

    /// <summary>
    /// Gets or sets the end month and year for the metrics data.
    /// </summary>
    public MonthYearDto ToMY { get; set; } = new();

    /// <summary>
    /// Gets or sets the category identifier for filtering metrics data.
    /// </summary>
    public Guid CategoryId { get; set; }

    /// <summary>
    /// Gets or sets the product identifier for filtering metrics data.
    /// If null, no product-specific filtering will be applied.
    /// </summary>
    public Guid? ProductId { get; set; }
}
