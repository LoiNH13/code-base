using Sale.Domain.Enumerations;

namespace Sale.Contract.Customers;

/// <summary>
/// Represents a request for retrieving customers with their associated plans.
/// </summary>
public class CustomerWithPlanRequest
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
    /// Gets or sets the unique identifier of the planning control.
    /// </summary>
    public Guid PlanningControlId { get; set; }

    /// <summary>
    /// Gets or sets the planning approval status.
    /// If null, all statuses are considered.
    /// </summary>
    public int? PlanningApprovalStatus { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the user who manages the customers.
    /// If null, all users are considered.
    /// </summary>
    public Guid? ManagedByUserId { get; set; }

    /// <summary>
    /// Gets or sets the search text to filter customers.
    /// If null or empty, no text filtering is applied.
    /// </summary>
    public string? SearchText { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether customers should have Odoo integration.
    /// If null, both with and without Odoo integration are considered.
    /// </summary>
    public ECustomerTag? ECustomerTag { get; set; }
}
