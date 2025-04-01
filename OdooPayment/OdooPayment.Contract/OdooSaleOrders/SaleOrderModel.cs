using Odoo.Domain.Entities;

namespace OdooPayment.Contract.OdooSaleOrders
{
    public class SaleOrderModel
    {
        public SaleOrderModel(ReportSaleOrderView so)
        {
            id = so.Id;
            create_date = so.CreateDate;
            write_date = so.WriteDate;
            order_id = so.OrderId;
            order_name = so.OrderName;
            so_type = so.SoType;
            customer_id = so.CustomerId;
            customer_name = so.CustomerName;
            internal_ref = so.InternalRef;
            client_order_ref = so.ClientOrderRef;
            so_reference_number = so.SoReferenceNumber;
            responsible = so.Responsible;
            date_dones = so.DateDones;
            date_invoices = so.DateInvoices?.ToDateTime(TimeOnly.MinValue);
            payment_due_date = so.PaymentDueDate?.ToDateTime(TimeOnly.MinValue);
            date_dues = so.DateDues?.ToDateTime(TimeOnly.MinValue);
            amount_total_invoice = so.AmountTotalInvoice;
            amount_paid_invoice = so.AmountPaidInvoice;
            warehouse = so.Warehouse;
            team_id = so.TeamId;
            team = so.Team;
            salesperson_id = so.SalespersonId;
            salesperson = so.Salesperson;
            shipping_type = so.ShippingType;
            channel = so.Channel;
            date_order = so.DateOrder?.ToDateTime(TimeOnly.MinValue);
            company_analytic_account = so.CompanyAnalyticAccount;
            payment_term = so.PaymentTerm;
            state_name = so.StateName;
            state = so.State;
            picking_status = so.PickingStatus;
            invoice_ref = so.InvoiceRef;
        }

        public int? id { get; set; }

        public DateTime? create_date { get; set; }

        public DateTime? write_date { get; set; }

        public int? order_id { get; set; }

        public string? order_name { get; set; }

        public string? so_type { get; set; }

        public int? customer_id { get; set; }

        public string? customer_name { get; set; }

        public string? internal_ref { get; set; }

        public string? client_order_ref { get; set; }

        public string? so_reference_number { get; set; }

        public string? responsible { get; set; }

        public DateTime? date_dones { get; set; }

        public DateTime? date_invoices { get; set; }

        public DateTime? payment_due_date { get; set; }

        public DateTime? date_dues { get; set; }

        public decimal? amount_total_invoice { get; set; }

        public decimal? amount_paid_invoice { get; set; }

        public string? warehouse { get; set; }

        public int? team_id { get; set; }

        public string? team { get; set; }

        public int? salesperson_id { get; set; }

        public string? salesperson { get; }

        public string? shipping_type { get; set; }

        public string? channel { get; set; }

        public DateTime? date_order { get; set; }

        public string? company_analytic_account { get; set; }

        public string? payment_term { get; set; }

        public string? state_name { get; set; }

        public string? state { get; set; }

        public string? picking_status { get; set; }

        public string? invoice_ref { get; set; }
    }
}
