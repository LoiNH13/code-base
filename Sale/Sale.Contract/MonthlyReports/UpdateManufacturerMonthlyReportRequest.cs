using Sale.Contract.MonthlyReportItems;

namespace Sale.Contract.MonthlyReports;

/// <summary>
/// Represents a request to update the manufacturer's monthly report.
/// </summary>
public class UpdateManufacturerMonthlyReportRequest
{
    /// <summary>
    /// Gets or sets the start time of the report period in UTC.
    /// </summary>
    public DateTime FromTimeOnUtc { get; set; }

    /// <summary>
    /// Gets or sets the end time of the report period in UTC.
    /// This field is optional and can be null.
    /// </summary>
    public DateTime? ToTimeOnUtc { get; set; }

    /// <summary>
    /// Gets or sets the number of new buys in the current month.
    /// </summary>
    public int NewBuyInMonth { get; set; }

    /// <summary>
    /// Gets or sets the number of new buys in the next month.
    /// </summary>
    public int NewBuyNextMonth { get; set; }

    /// <summary>
    /// Gets or sets the service amount in the current month.
    /// </summary>
    public double ServiceInMonth { get; set; }

    /// <summary>
    /// Gets or sets the note for the report.
    /// This field is optional and can be null.
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Gets or sets the items for the report.
    /// </summary>
    public List<UpdateMonthlyReportItem2Request>? Items { get; set; }
}