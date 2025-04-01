using Sale.Contract.PlanNewCustomers;
using Sale.Contract.PlanningApprovals;

namespace Sale.Contract.Customers;

/// <summary>
/// Represents a customer with associated plan and planning approval information.
/// </summary>
public class CustomerWithPlanResponse
{
    /// <summary>
    /// The unique identifier of the customer.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The name of the customer.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// The Odoo reference number of the customer.
    /// </summary>
    public int? OdooRef { get; set; }

    /// <summary>
    /// The unique identifier of the user who manages the customer.
    /// </summary>
    public Guid? ManagedByUserId { get; set; }

    /// <summary>
    /// The details of the new customer plan associated with the customer.
    /// </summary>
    public PlanNewCustomerResponse? PlanNewCustomer { get; set; }

    /// <summary>
    /// The details of the planning approval associated with the customer.
    /// </summary>
    public PlanningApprovalResponse? PlanningApproval { get; set; }
}