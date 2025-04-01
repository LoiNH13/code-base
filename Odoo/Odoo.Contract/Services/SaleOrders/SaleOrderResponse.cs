using System.Text.Json.Serialization;

namespace Odoo.Contract.Services.SaleOrders
{
    public class SaleOrderResponse
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        [JsonPropertyName("so_reference_id")]
        public string? SoReferenceId { get; set; }

        public required string State { get; set; }

        [JsonPropertyName("invoice_ids")]
        public List<Invoice>? InvoiceIds { get; set; }
    }

    public class Invoice
    {
        public int Id { get; set; }

        public required string State { get; set; }

        public required string Number { get; set; }

        public required string Origin { get; set; }

        [JsonPropertyName("create_date")]
        public required string CreateDate { get; set; }

        [JsonPropertyName("write_date")]
        public string? WriteDate { get; set; }

        [JsonPropertyName("amount_total")]
        public double AmountTotal { get; set; }

        public double Residual { get; set; }

        [JsonPropertyName("payment_ids")]
        public List<Payment>? PaymentIds { get; set; }

        [JsonPropertyName("invoice_line_ids")]
        public List<InvoiceLine>? InvoiceLineIds { get; set; }
    }

    public class Payment
    {
        public required string Name { get; set; }

        [JsonPropertyName("internal_reference")]
        public bool? InternalReference { get; set; }

        public double Amount { get; set; }

        [JsonPropertyName("payment_date")]
        public required string PaymentDate { get; set; }

        public int Id { get; set; }
    }

    public class InvoiceLine
    {
        public int Id { get; set; }

        [JsonPropertyName("product_id")]
        public Product? ProductId { get; set; }

        [JsonPropertyName("price_unit")]
        public double PriceUnit { get; set; }

        public double Quantity { get; set; }

        [JsonPropertyName("price_total")]
        public double PriceTotal { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }

        [JsonPropertyName("display_name")]
        public required string DisplayName { get; set; }
    }
}
