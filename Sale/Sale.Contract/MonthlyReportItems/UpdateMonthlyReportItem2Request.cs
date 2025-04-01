namespace Sale.Contract.MonthlyReportItems;

public class UpdateMonthlyReportItem2Request
{
    /// <summary>
    /// Gets or sets the identifier of the monthly report item to update.
    /// </summary>
    public Guid Id { get; set; }

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
