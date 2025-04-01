namespace Sale.Contract.MonthlyReportItems;

/// <summary>
/// Represents a request to update a monthly report item.
/// </summary>
public class UpdateMonthlyReportItemRequest
{
    /// <summary>
    /// Gets or sets the updated quantity for the monthly report item.
    /// </summary>
    public double Quantity { get; set; }

    /// <summary>
    /// Gets or sets the updated revenue for the monthly report item.
    /// </summary>
    public double Revenue { get; set; }

    /// <summary>
    /// Gets or sets the updated note for the monthly report item.
    /// This field is optional and can be null.
    /// </summary>
    public string? Note { get; set; }
}