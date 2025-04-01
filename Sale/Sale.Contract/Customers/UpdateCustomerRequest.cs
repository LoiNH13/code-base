using Sale.Domain.Enumerations;

namespace Sale.Contract.Customers;

/// <summary>
/// Represents a request to update customer information.
/// </summary>
public class UpdateCustomerRequest
{
    /// <summary>
    /// Gets or sets the user identifier who is managing the customer.
    /// If null, the customer's management is not changed.
    /// </summary>
    public Guid? ManagedByUserId { get; set; }

    /// <summary>
    /// Gets or sets the number of visits per month for the customer.
    /// </summary>
    public int VitsitPerMonth { get; set; }

    /// <summary>
    /// Gets or sets the business type of the customer.
    /// </summary>
    public EBusinessType BusinessType { get; set; }
}