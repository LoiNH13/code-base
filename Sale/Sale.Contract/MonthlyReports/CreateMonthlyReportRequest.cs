using Sale.Contract.MonthlyReportItems;

namespace Sale.Contract.MonthlyReports;

/// <summary>
/// Represents a request to create a monthly report.
/// </summary>
public class CreateMonthlyReportRequest
{
    /// <summary>
    /// The start date and time of the report period in UTC.
    /// </summary>
    public DateTime FromTimeOnUtc { get; set; }

    /// <summary>
    /// The end date and time of the report period in UTC. If null, the report covers up to the current date and time.
    /// </summary>
    public DateTime? ToTimeOnUtc { get; set; }

    /// <summary>
    /// The total number of daily visitors during the report period.
    /// </summary>
    public int DailyVisitors { get; set; }

    /// <summary>
    /// The total number of daily purchases during the report period.
    /// </summary>
    public int DailyPurchases { get; set; }

    /// <summary>
    /// The percentage of purchases made online during the report period.
    /// </summary>
    public double OnlinePurchaseRate { get; set; }

    /// <summary>
    /// Additional notes or comments related to the report.
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// The ID of the corresponding customer in the Odoo system.
    /// </summary>
    public int OdooCustomerId { get; set; }

    /// <summary>
    /// The list of items included in the monthly report.
    /// </summary>
    public List<CreateMonthlyReportItemRequest>? Items { get; set; }
}
