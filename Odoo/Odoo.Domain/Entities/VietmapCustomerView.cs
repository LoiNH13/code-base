namespace Odoo.Domain.Entities;

public partial class VietmapCustomerView
{
    public string? CustomerAddress { get; set; }

    public string? District { get; set; }

    public string? Name { get; set; }

    public int? Id { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? StateName { get; set; }

    public DateTime? WriteDate { get; set; }

    public bool? ActiveCustomer { get; set; }

    public string? Mobile { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? InternalRef { get; set; }

    public string? Reference { get; set; }

    public decimal? Credit { get; set; }

    public double? CreditLimit { get; set; }

    public int? EmployeeId { get; set; }

    public string? EmployeeName { get; set; }

    public string? CarCompany { get; set; }

    public string? Pricelist { get; set; }

    public int? PricelistId { get; set; }

    public string? Scale { get; set; }

    public string? PartnerGroup { get; set; }

    public string? Category { get; set; }

    public string? CreateUserEmail { get; set; }
}
