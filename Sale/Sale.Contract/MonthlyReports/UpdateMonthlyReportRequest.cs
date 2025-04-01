
using Sale.Contract.MonthlyReportItems;

namespace Sale.Contract.MonthlyReports;

/// <summary>
/// Represents a request to update a monthly report.
/// </summary>
public class UpdateMonthlyReportRequest
{
    /// <summary>
    /// Gets or sets the start date and time of the report in UTC.
    /// </summary>
    public DateTime FromTimeOnUtc { get; set; }

    /// <summary>
    /// Gets or sets the end date and time of the report in UTC.
    /// If null, the report covers up to the current date and time.
    /// </summary>
    public DateTime? ToTimeOnUtc { get; set; }

    /// <summary>
    /// Gets or sets the number of daily visitors for the report.
    /// </summary>
    public int DailyVisitors { get; set; }

    /// <summary>
    /// Gets or sets the number of daily purchases for the report.
    /// </summary>
    public int DailyPurchases { get; set; }

    /// <summary>
    /// Gets or sets the online purchase rate for the report.
    /// The value should be a percentage (e.g., 0.25 for 25%).
    /// </summary>
    public double OnlinePurchaseRate { get; set; }

    /// <summary>
    /// Gets or sets any additional notes for the report.
    /// This field is optional and can be null.
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Gets or sets the items for the report.
    /// </summary>
    public List<UpdateMonthlyReportItem2Request>? Items { get; set; }
}