using Sale.Domain.Enumerations;

namespace Sale.Contract.Customers;

/// <summary>
/// Represents a request to create a new customer in Odoo.
/// </summary>
public class CreateOdooCustomerRequest
{
    /// <summary>
    /// The unique identifier of the customer in Odoo.
    /// </summary>
    public int OdooRef { get; set; }

    /// <summary>
    /// The estimated number of visits per month for the customer.
    /// </summary>
    public int VisitPerMonth { get; set; }

    /// <summary>
    /// The unique identifier of the user who is managing the customer.
    /// This field is optional and can be null if not specified.
    /// </summary>
    public Guid? ManagedByUserId { get; set; }


    /// <summary>
    /// The business type of the customer.
    /// </summary>
    public EBusinessType BusinessType { get; set; }
}
