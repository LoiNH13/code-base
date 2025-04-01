using Sale.Contract.MonthlyReports;
using Sale.Contract.PlanNewCustomers;
using Sale.Contract.Users;
using Sale.Domain.Entities.Customers;
using Sale.Domain.Entities.MonthlyReports;
using Sale.Domain.Entities.Users;
using Sale.Domain.Enumerations;

namespace Sale.Contract.Customers;

/// <summary>
/// Represents a customer response object.
/// </summary>
public class CustomerResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the customer.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the Odoo reference number for the customer.
    /// </summary>
    public int? OdooRef { get; set; }

    /// <summary>
    /// Gets or sets the name of the customer.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the business type of the customer.
    /// </summary>
    public EBusinessType BusinessType { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the user who manages the customer.
    /// </summary>
    public Guid? ManagedByUserId { get; set; }

    /// <summary>
    /// Gets or sets the number of visits per month for the customer.
    /// </summary>
    public int VisitPerMonth { get; set; }

    public HashSet<ECustomerTag> Tags { get; } = new HashSet<ECustomerTag>();

    /// <summary>
    /// Gets or sets the plan new customer response associated with the customer.
    /// </summary>
    public PlanNewCustomerResponse? PlanNewCustomer { get; set; }

    /// <summary>
    /// Gets or sets the user response associated with the customer.
    /// </summary>
    public UserResponse? UserResponse { get; set; }

    public List<MonthlyReportResponse>? MonthlyReports { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomerResponse"/> class.
    /// </summary>
    /// <param name="customer">The customer entity to create the response from.</param>
    public CustomerResponse(Customer customer)
    {
        Id = customer.Id;
        OdooRef = customer.OdooRef;
        Name = customer.Name;
        ManagedByUserId = customer.ManagedByUserId;
        BusinessType = customer.BusinessType;
        VisitPerMonth = customer.VisitPerMonth;
        PlanNewCustomer = customer.PlanNewCustomer is not null
            ? new PlanNewCustomerResponse(customer.PlanNewCustomer)
            : null;
        if (OdooRef is null || PlanNewCustomer is not null) Tags.Add(ECustomerTag.PlanNew);
        if (OdooRef is not null) Tags.Add(ECustomerTag.Opened);
        if (Tags.Count == 0) Tags.Add(ECustomerTag.Unknow);
    }

    /// <summary>
    /// Creates a new <see cref="CustomerResponse"/> instance with associated user information.
    /// </summary>
    /// <param name="customer">The customer entity to create the response from.</param>
    /// <param name="user">The user entity associated with the customer.</param>
    /// <returns>A new <see cref="CustomerResponse"/> instance.</returns>
    public static CustomerResponse Create(Customer customer, User? user)
    {
        CustomerResponse customerResponse = new CustomerResponse(customer);
        customerResponse.UserResponse = user is not null ? new UserResponse(user) : null;
        return customerResponse;
    }

    public static CustomerResponse Create(Customer customer, List<MonthlyReport>? monthlyReports)
    {
        CustomerResponse customerResponse = new CustomerResponse(customer);
        if (monthlyReports is not null)
            customerResponse.MonthlyReports = monthlyReports.Select(report => new MonthlyReportResponse(report)).ToList();
        return customerResponse;
    }
}