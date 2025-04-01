using Sale.Contract.Customers;

namespace Sale.Contract.Users;

/// <summary>
/// Represents a response containing user information along with a list of customers' summary with their respective plan information.
/// </summary>
public class UsersWithPlanApprovalResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the user.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets a list of <see cref="SummaryCustomersWithPlanResponse"/> objects representing customers' summary with their respective plan information.
    /// </summary>
    public List<SummaryCustomersWithPlanResponse> SummaryCustomers { get; set; } = new();
}