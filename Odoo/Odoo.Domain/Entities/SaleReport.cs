namespace Odoo.Domain.Entities;

public partial class SaleReport
{
    public int? Id { get; set; }

    public int? ProductId { get; set; }

    public int? ProductUom { get; set; }

    public double? ProductUomQty { get; set; }

    public double? QtyDelivered { get; set; }

    public double? QtyToDeliver { get; set; }

    public double? QtyInvoiced { get; set; }

    public double? QtyToInvoice { get; set; }

    public double? PriceTotal { get; set; }

    public double? PriceSubtotal { get; set; }

    public double? UntaxedAmountToInvoice { get; set; }

    public double? UntaxedAmountInvoiced { get; set; }

    public long? Nbr { get; set; }

    public string? Name { get; set; }

    public DateTime? Date { get; set; }

    public string? State { get; set; }

    public string? InvoiceStatus { get; set; }

    public int? PartnerId { get; set; }

    public int? UserId { get; set; }

    public int? CompanyId { get; set; }

    public int? CampaignId { get; set; }

    public int? MediumId { get; set; }

    public int? SourceId { get; set; }

    public int? CategId { get; set; }

    public int? PricelistId { get; set; }

    public int? AnalyticAccountId { get; set; }

    public int? TeamId { get; set; }

    public int? ProductTmplId { get; set; }

    public int? CommercialPartnerId { get; set; }

    public int? CountryId { get; set; }

    public int? IndustryId { get; set; }

    public int? StateId { get; set; }

    public string? PartnerZip { get; set; }

    public double? Weight { get; set; }

    public double? Volume { get; set; }

    public double? Discount { get; set; }

    public double? DiscountAmount { get; set; }

    public string? OrderReference { get; set; }

    public int? WarehouseId { get; set; }
}
