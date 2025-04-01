using Sale.Contract.Categories;
using Sale.Contract.TimeFrames;

namespace Sale.Contract.Metrics;

/// <summary>
/// Represents a summary response for sales metrics by customer.
/// </summary>
public class SummaryByCustomerResponse
{
    /// <summary>
    /// Gets or sets the category for which the summary is calculated.
    /// </summary>
    public CategoryResponse? Category { get; set; }

    /// <summary>
    /// Gets or sets a list of summary metrics for each category and time frame.
    /// </summary>
    public List<SummaryMetricByCategoryTimeFrame> CategoryTimeFrames { get; set; } = new();

    /// <summary>
    /// Calculates the total sales amount for the category across all time frames.
    /// </summary>
    public double TotalByCategory => CategoryTimeFrames.Sum(x => x.TotalByCategoryTimeFrame);
}

/// <summary>
/// Represents a summary metric for sales by customer and category for a specific time frame.
/// </summary>
public class SummaryMetricByCategoryTimeFrame
{
    /// <summary>
    /// Gets or sets the time frame for which the summary metric is calculated.
    /// </summary>
    /// <remarks>
    /// This property can be null if the summary is not associated with a specific time frame.
    /// </remarks>
    public TimeFrameResponse? TimeFrame { get; set; }

    /// <summary>
    /// Gets or sets the total sales amount for the category and time frame.
    /// </summary>
    public double TotalByCategoryTimeFrame { get; set; }
}