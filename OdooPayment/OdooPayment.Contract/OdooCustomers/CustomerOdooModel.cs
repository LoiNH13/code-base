using Odoo.Domain.Entities;
using OdooPayment.Contract.OdooSaleOrders;

namespace OdooPayment.Contract.OdooCustomers;

public class CustomerOdooModel
{
    public string? customer_address { get; set; }

    public string? name { get; set; }

    public int? id { get; set; }

    public DateTime? create_date { get; set; }

    public DateTime? write_date { get; set; }

    public bool? active_customer { get; set; }

    public string? mobile { get; set; }

    public string? email { get; set; }

    public string? internal_ref { get; set; }

    public decimal? credit { get; set; }

    public double? credit_limit { get; set; }

    public string? employee_id { get; set; }

    public string? employee_name { get; set; }

    public int? count_so { get; set; }

    public double? total { get; set; }

    public double? paid { get; set; }

    public IEnumerable<SaleOrderModel>? SaleOrders { get; set; }

    public CustomerOdooModel(VietmapCustomerView customer)
    {
        customer_address = customer.CustomerAddress;
        name = customer.Name;
        id = customer.Id;
        create_date = customer.CreateDate;
        write_date = customer.WriteDate;
        active_customer = customer.ActiveCustomer;
        mobile = customer.Mobile;
        email = customer.Email;
        internal_ref = customer.InternalRef;
        credit = customer.Credit;
        credit_limit = customer.CreditLimit;
        employee_id = customer.EmployeeId.ToString();
        employee_name = customer.EmployeeName;
    }

    public static CustomerOdooModel Create(VietmapCustomerView customer, List<ReportSaleOrderView> sos)
    {
        var customerOdooModel = new CustomerOdooModel(customer);
        customerOdooModel.SaleOrders = sos.Select(so => new SaleOrderModel(so)).ToList();
        return customerOdooModel;
    }
}
