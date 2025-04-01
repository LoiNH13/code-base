namespace Sale.Contract.Odoo.Customers;

/// <summary>
/// Represents a response from Odoo for a customer.
/// </summary>
public class OdooCustomerResponse
{
    /// <summary>
    /// Gets or sets the customer's name.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the customer's unique identifier.
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// Gets or sets the customer's internal reference.
    /// </summary>
    public string? InternalRef { get; set; }

    /// <summary>
    /// Gets a formatted string combining the customer's internal reference and name, separated by a hyphen.
    /// </summary>
    public string DTWithName => string.Join("-", InternalRef, Name);
}
