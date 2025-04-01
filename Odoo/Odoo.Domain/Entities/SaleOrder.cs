using System;
using System.Collections.Generic;

namespace Odoo.Domain.Entities;

/// <summary>
/// Sales Order
/// </summary>
public partial class SaleOrder
{
    public int Id { get; set; }

    /// <summary>
    /// Campaign
    /// </summary>
    public int? CampaignId { get; set; }

    /// <summary>
    /// Source
    /// </summary>
    public int? SourceId { get; set; }

    /// <summary>
    /// Medium
    /// </summary>
    public int? MediumId { get; set; }

    /// <summary>
    /// Company
    /// </summary>
    public int CompanyId { get; set; }

    /// <summary>
    /// Customer
    /// </summary>
    public int PartnerId { get; set; }

    /// <summary>
    /// Invoicing Journal
    /// </summary>
    public int? JournalId { get; set; }

    /// <summary>
    /// Invoice Address
    /// </summary>
    public int PartnerInvoiceId { get; set; }

    /// <summary>
    /// Delivery Address
    /// </summary>
    public int? PartnerShippingId { get; set; }

    /// <summary>
    /// Fiscal Position
    /// </summary>
    public int? FiscalPositionId { get; set; }

    /// <summary>
    /// Payment Terms
    /// </summary>
    public int? PaymentTermId { get; set; }

    /// <summary>
    /// Pricelist
    /// </summary>
    public int? PricelistId { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    public int? CurrencyId { get; set; }

    /// <summary>
    /// Salesperson
    /// </summary>
    public int? UserId { get; set; }

    /// <summary>
    /// Sales Team
    /// </summary>
    public int? TeamId { get; set; }

    /// <summary>
    /// Analytic Account
    /// </summary>
    public int? AnalyticAccountId { get; set; }

    /// <summary>
    /// Created by
    /// </summary>
    public int? CreateUid { get; set; }

    /// <summary>
    /// Last Updated by
    /// </summary>
    public int? WriteUid { get; set; }

    /// <summary>
    /// Security Token
    /// </summary>
    public string? AccessToken { get; set; }

    /// <summary>
    /// Order Reference
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Status
    /// </summary>
    public string? State { get; set; }

    /// <summary>
    /// Customer Reference
    /// </summary>
    public string? ClientOrderRef { get; set; }

    /// <summary>
    /// Source Document
    /// </summary>
    public string? Origin { get; set; }

    /// <summary>
    /// Payment Ref.
    /// </summary>
    public string? Reference { get; set; }

    /// <summary>
    /// Signed By
    /// </summary>
    public string? SignedBy { get; set; }

    /// <summary>
    /// Invoice Status
    /// </summary>
    public string? InvoiceStatus { get; set; }

    /// <summary>
    /// Expiration
    /// </summary>
    public DateOnly? ValidityDate { get; set; }

    /// <summary>
    /// Terms and conditions
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Currency Rate
    /// </summary>
    public decimal CurrencyRate { get; set; }

    /// <summary>
    /// Untaxed Amount
    /// </summary>
    public decimal? AmountUntaxed { get; set; }

    /// <summary>
    /// Taxes
    /// </summary>
    public decimal? AmountTax { get; set; }

    /// <summary>
    /// Total
    /// </summary>
    public decimal? AmountTotal { get; set; }

    /// <summary>
    /// Amount to invoice
    /// </summary>
    public decimal? AmountToInvoice { get; set; }

    /// <summary>
    /// Locked
    /// </summary>
    public bool? Locked { get; set; }

    /// <summary>
    /// Online signature
    /// </summary>
    public bool? RequireSignature { get; set; }

    /// <summary>
    /// Online payment
    /// </summary>
    public bool? RequirePayment { get; set; }

    /// <summary>
    /// Creation Date
    /// </summary>
    public DateTime? CreateDate { get; set; }

    /// <summary>
    /// Delivery Date
    /// </summary>
    public DateTime? CommitmentDate { get; set; }

    /// <summary>
    /// Order Date
    /// </summary>
    public DateTime DateOrder { get; set; }

    /// <summary>
    /// Signed On
    /// </summary>
    public DateTime? SignedOn { get; set; }

    /// <summary>
    /// Last Updated on
    /// </summary>
    public DateTime? WriteDate { get; set; }

    /// <summary>
    /// Prepayment percentage
    /// </summary>
    public double? PrepaymentPercent { get; set; }

    /// <summary>
    /// Pending Email Template
    /// </summary>
    public int? PendingEmailTemplateId { get; set; }

    /// <summary>
    /// Incoterm
    /// </summary>
    public int? Incoterm { get; set; }

    /// <summary>
    /// Warehouse
    /// </summary>
    public int WarehouseId { get; set; }

    /// <summary>
    /// Procurement Group
    /// </summary>
    public int? ProcurementGroupId { get; set; }

    /// <summary>
    /// Incoterm Location
    /// </summary>
    public string? IncotermLocation { get; set; }

    /// <summary>
    /// Shipping Policy
    /// </summary>
    public string PickingPolicy { get; set; } = null!;

    /// <summary>
    /// Delivery Status
    /// </summary>
    public string? DeliveryStatus { get; set; }

    /// <summary>
    /// Effective Date
    /// </summary>
    public DateTime? EffectiveDate { get; set; }

    public int? SaleOrderTemplateId { get; set; }

    /// <summary>
    /// Delivery Orders
    /// </summary>
    public int? DeliveryCount { get; set; }

    /// <summary>
    /// Original SO Number
    /// </summary>
    public int? OriginalOrderId { get; set; }

    /// <summary>
    /// Return Procurement Group
    /// </summary>
    public int? ReturnProcurementGroupId { get; set; }

    /// <summary>
    /// Picking Return Count
    /// </summary>
    public int? PickingCountReturn { get; set; }

    /// <summary>
    /// Company Currency
    /// </summary>
    public int? CompanyCurrencyId { get; set; }

    /// <summary>
    /// Order Type
    /// </summary>
    public string? OrderType { get; set; }

    /// <summary>
    /// Input Original SO Number
    /// </summary>
    public string? InputOrderNumber { get; set; }

    /// <summary>
    /// Return Reason
    /// </summary>
    public string? ReturnReason { get; set; }

    /// <summary>
    /// Total Delivered Qty
    /// </summary>
    public double? TotalDeliveredQty { get; set; }

    /// <summary>
    /// Total Delivered Value
    /// </summary>
    public double? TotalDeliveredValue { get; set; }

    /// <summary>
    /// Estimated Delivery Price
    /// </summary>
    public double? DeliveryPrice { get; set; }

    /// <summary>
    /// Total Quantity
    /// </summary>
    public double? TotalQuantity { get; set; }

    /// <summary>
    /// Opportunity
    /// </summary>
    public int? OpportunityId { get; set; }

    /// <summary>
    /// Sales Channel
    /// </summary>
    public int? ChannelId { get; set; }

    /// <summary>
    /// First Approver
    /// </summary>
    public int? FirstApproverRecordId { get; set; }

    /// <summary>
    /// Second Approver
    /// </summary>
    public int? SecondApproverRecordId { get; set; }

    /// <summary>
    /// Shipping Type
    /// </summary>
    public int? ShippingTypeIds { get; set; }

    /// <summary>
    /// SO Type
    /// </summary>
    public int? SoTypeIds { get; set; }

    /// <summary>
    /// Return Reason
    /// </summary>
    public int? ReturnReasonId { get; set; }

    /// <summary>
    /// Res Partner Group
    /// </summary>
    public int? PartnerGroupId { get; set; }

    /// <summary>
    /// Reference SO
    /// </summary>
    public string? SoReferenceId { get; set; }

    /// <summary>
    /// Address
    /// </summary>
    public string? PartnerAddress { get; set; }

    /// <summary>
    /// Responsible
    /// </summary>
    public string? Responsible { get; set; }

    /// <summary>
    /// Date invoice
    /// </summary>
    public DateOnly? DateInvoices { get; set; }

    /// <summary>
    /// Date due
    /// </summary>
    public DateOnly? DateDues { get; set; }

    /// <summary>
    /// Payment Due Day
    /// </summary>
    public DateOnly? PaymentDueDate { get; set; }

    /// <summary>
    /// Amount total
    /// </summary>
    public decimal? AmountTotalInvoice { get; set; }

    /// <summary>
    /// Amount paid
    /// </summary>
    public decimal? AmountPaidInvoice { get; set; }

    /// <summary>
    /// Picking Checked
    /// </summary>
    public bool? PickingChecked { get; set; }

    /// <summary>
    /// Date done
    /// </summary>
    public DateTime? DateDones { get; set; }

    /// <summary>
    /// Vat Partner
    /// </summary>
    public int? VatPartnerId { get; set; }

    /// <summary>
    /// Need Create SO V15
    /// </summary>
    public bool? IsNeedCreateSo15 { get; set; }

    /// <summary>
    /// Need Create Consignment V15
    /// </summary>
    public bool? IsNeedCreateConsignment15 { get; set; }

    /// <summary>
    /// Source Purchase Order
    /// </summary>
    public int? AutoPurchaseOrderId { get; set; }

    /// <summary>
    /// Auto Generated Sales Order
    /// </summary>
    public bool? AutoGenerated { get; set; }

    /// <summary>
    /// Buyer Name on VAT Invoice
    /// </summary>
    public string? BuyerNameVatInvoice { get; set; }

    /// <summary>
    /// VAT Number
    /// </summary>
    public string? VatNumber { get; set; }

    /// <summary>
    /// VAT Invoice Type
    /// </summary>
    public string? VatInvoiceType { get; set; }

    /// <summary>
    /// VAT Invoice Status
    /// </summary>
    public string? VatInvoiceStatus { get; set; }

    /// <summary>
    /// VAT Date
    /// </summary>
    public DateOnly? VatDate { get; set; }

    /// <summary>
    /// Company Dest
    /// </summary>
    public int? DestCompanyId { get; set; }

    /// <summary>
    /// Dest Warehouse (Inter-comp)
    /// </summary>
    public int? DestWarehouseId { get; set; }

    /// <summary>
    /// Hide Reverse Tranfers
    /// </summary>
    public bool? ComputeHideReverseTranfers { get; set; }

    /// <summary>
    /// Is Inter-company Transaction ?
    /// </summary>
    public bool? IsInterCompanyTransaction { get; set; }

    /// <summary>
    /// API Source
    /// </summary>
    public int? ApiSoConfig { get; set; }

    /// <summary>
    /// Auto Reconcile
    /// </summary>
    public string? PaymentPayload { get; set; }

    /// <summary>
    /// Platform?
    /// </summary>
    public bool? Platform { get; set; }

    public virtual ICollection<AccountMove> AccountMoves { get; set; } = new List<AccountMove>();

    public virtual AccountAnalyticAccount? AnalyticAccount { get; set; }

    public virtual ResUser? CreateU { get; set; }

    public virtual ResUser? FirstApproverRecord { get; set; }

    public virtual ICollection<SaleOrder> InverseOriginalOrder { get; set; } = new List<SaleOrder>();

    public virtual AccountJournal? Journal { get; set; }

    public virtual SaleOrder? OriginalOrder { get; set; }

    public virtual ResPartner Partner { get; set; } = null!;

    public virtual ResPartner PartnerInvoice { get; set; } = null!;

    public virtual ResPartner? PartnerShipping { get; set; }

    public virtual ResUser? SecondApproverRecord { get; set; }

    public virtual ResUser? User { get; set; }

    public virtual ResPartner? VatPartner { get; set; }

    public virtual ResUser? WriteU { get; set; }
}
