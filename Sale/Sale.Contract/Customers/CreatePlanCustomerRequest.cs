namespace Sale.Contract.Customers;

/// <summary>
/// Represents a request to create a new customer for a plan.
/// </summary>
public class CreatePlanCustomerRequest
{
    /// <summary>
    /// Gets or sets the ID of the city where the customer resides.
    /// </summary>
    public int CityId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the district where the customer resides.
    /// </summary>
    public int DistrictId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the ward where the customer resides.
    /// This property is optional and can be null if the customer does not reside in a specific ward.
    /// </summary>
    public int? WardId { get; set; }
}
