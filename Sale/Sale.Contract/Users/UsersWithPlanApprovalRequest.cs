namespace Sale.Contract.Users;

/// <summary>
/// Represents a request for retrieving users associated with a specific planning control,
/// with optional filtering options for status and Odoo presence.
/// </summary>
public class UsersWithPlanApprovalRequest
{
    /// <summary>
    /// The unique identifier of the planning control.
    /// </summary>
    public Guid PlanningControlId { get; set; }

    /// <summary>
    /// The status of the users to be retrieved. If null, all statuses are considered.
    /// </summary>
    public int? Status { get; set; }

    /// <summary>
    /// Indicates whether users with Odoo presence should be retrieved. If null, both with and without Odoo presence are considered.
    /// </summary>
    public bool? IsHaveOdoo { get; set; }

    /// <summary>
    /// A list of unique identifiers of users who are managed by the requesting user.
    /// </summary>
    public required List<Guid> ManagedByUserIds { get; set; }
}