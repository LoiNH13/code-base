using Sale.Domain.Enumerations;

namespace Sale.Contract.Customers;

/// <summary>
/// Represents a request for retrieving a summary of customers with their associated plans.
/// </summary>
public sealed class SummaryCustomersWithPlanRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the planning control.
    /// </summary>
    public Guid PlanningControlId { get; set; }

    /// <summary>
    /// Gets or sets the status of the customers. If null, all statuses are considered.
    /// </summary>
    public int? Status { get; set; }

    /// <summary>
    /// Gets or sets the search text to filter customers by name or code. If null, no filtering is applied.
    /// </summary>
    public string? SearchText { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the user who is managing the customers. If null, all users are considered.
    /// </summary>
    public Guid? ManagedByUserId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether customers should have an associated Odoo record. If null, no filtering is applied.
    /// </summary>
    public ECustomerTag? CustomerTag { get; set; }
}
