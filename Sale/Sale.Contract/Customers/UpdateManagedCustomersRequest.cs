namespace Sale.Contract.Customers;

/// <summary>
/// Represents a request to update managed customers for a specific user.
/// </summary>
public class UpdateManagedCustomersRequest
{
    /// <summary>
    /// The unique identifier of the user whose managed customers are being updated.
    /// If null, the operation will apply to all users.
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    /// A list of unique identifiers of the customers to be updated.
    /// If the list is empty, no customers will be updated.
    /// </summary>
    public List<Guid> CustomerIds { get; set; } = new();
}
