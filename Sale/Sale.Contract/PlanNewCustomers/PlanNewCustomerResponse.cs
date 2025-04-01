using Sale.Domain.Entities.Customers;

namespace Sale.Contract.PlanNewCustomers;

/// <summary>
/// Represents a response for a plan new customer.
/// </summary>
public class PlanNewCustomerResponse
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlanNewCustomerResponse"/> class.
    /// </summary>
    /// <param name="planNewCustomer">The source plan new customer entity.</param>
    public PlanNewCustomerResponse(PlanNewCustomer planNewCustomer)
    {
        Id = planNewCustomer.Id;
        CustomerId = planNewCustomer.CustomerId;
        Code = planNewCustomer.Code;
        CityId = planNewCustomer.CityId;
        DistrictId = planNewCustomer.DistrictId;
        WardId = planNewCustomer.WardId;
        IsOpen = planNewCustomer.IsOpen;
    }

    /// <summary>
    /// Gets the unique identifier of the plan new customer.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Gets the unique identifier of the customer.
    /// </summary>
    public Guid CustomerId { get; }

    /// <summary>
    /// Gets the code of the plan new customer.
    /// </summary>
    public string Code { get; }

    /// <summary>
    /// Gets the city identifier.
    /// </summary>
    public int CityId { get; }

    /// <summary>
    /// Gets the city name.
    /// </summary>
    public string? CityName { get; private set; }

    /// <summary>
    /// Gets the district identifier.
    /// </summary>
    public int? DistrictId { get; }

    /// <summary>
    /// Gets the district name.
    /// </summary>
    public string? DistrictName { get; private set; }

    /// <summary>
    /// Gets the ward identifier.
    /// </summary>
    public int? WardId { get; }

    /// <summary>
    /// Gets the ward name.
    /// </summary>
    public string? WardName { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the plan new customer is open.
    /// </summary>
    public bool IsOpen { get; }

    /// <summary>
    /// Adds the city, district, and ward names to the response.
    /// </summary>
    /// <param name="cityName">The city name.</param>
    /// <param name="districtName">The district name.</param>
    /// <param name="wardName">The ward name.</param>
    public void AddName(string cityName, string? districtName, string? wardName)
    {
        CityName = cityName;
        DistrictName = districtName;
        WardName = wardName;
    }
}
