namespace Odoo.Contract.Invoices
{
    public class ConfirmInvoiceRequest
    {
        public required string Origin { get; set; }

        public required string CompanyAnalyticAccount { get; set; }

        public required string ConfirmMethod { get; set; }
    }
}
