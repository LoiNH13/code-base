
using Sale.Domain.Enumerations;

namespace Sale.Contract.MonthlyReportItems;

/// <summary>
/// Represents a request to create a new monthly report item.
/// </summary>
public class CreateMonthlyReportItemRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the category for this monthly report item.
    /// </summary>
    public Guid CategoryId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the item sold during the month.
    /// </summary>
    public double Quantity { get; set; }

    /// <summary>
    /// Gets or sets the total revenue generated from selling the item during the month.
    /// </summary>
    public double Revenue { get; set; }

    /// <summary>
    /// Gets or sets an optional note or comment related to this monthly report item.
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Gets or sets the group to which this monthly report item belongs.
    /// </summary>
    public EMonthlyReportItem? Group { get; set; }
}