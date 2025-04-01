using System;
using System.Collections.Generic;

namespace Odoo.Domain.Entities;

/// <summary>
/// Journal Entry
/// </summary>
public partial class AccountMove
{
    public int Id { get; set; }

    /// <summary>
    /// Sequence Number
    /// </summary>
    public int? SequenceNumber { get; set; }

    /// <summary>
    /// Main Attachment
    /// </summary>
    public int? MessageMainAttachmentId { get; set; }

    /// <summary>
    /// Journal
    /// </summary>
    public int JournalId { get; set; }

    /// <summary>
    /// Company
    /// </summary>
    public int? CompanyId { get; set; }

    /// <summary>
    /// Payment
    /// </summary>
    public int? PaymentId { get; set; }

    /// <summary>
    /// Statement Line
    /// </summary>
    public int? StatementLineId { get; set; }

    /// <summary>
    /// Tax Cash Basis Entry of
    /// </summary>
    public int? TaxCashBasisRecId { get; set; }

    /// <summary>
    /// Cash Basis Origin
    /// </summary>
    public int? TaxCashBasisOriginMoveId { get; set; }

    /// <summary>
    /// First recurring entry
    /// </summary>
    public int? AutoPostOriginId { get; set; }

    /// <summary>
    /// Inalteralbility No Gap Sequence #
    /// </summary>
    public int? SecureSequenceNumber { get; set; }

    /// <summary>
    /// Payment Terms
    /// </summary>
    public int? InvoicePaymentTermId { get; set; }

    /// <summary>
    /// Partner
    /// </summary>
    public int? PartnerId { get; set; }

    /// <summary>
    /// Commercial Entity
    /// </summary>
    public int? CommercialPartnerId { get; set; }

    /// <summary>
    /// Delivery Address
    /// </summary>
    public int? PartnerShippingId { get; set; }

    /// <summary>
    /// Recipient Bank
    /// </summary>
    public int? PartnerBankId { get; set; }

    /// <summary>
    /// Fiscal Position
    /// </summary>
    public int? FiscalPositionId { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    public int CurrencyId { get; set; }

    /// <summary>
    /// Reversal of
    /// </summary>
    public int? ReversedEntryId { get; set; }

    /// <summary>
    /// Salesperson
    /// </summary>
    public int? InvoiceUserId { get; set; }

    /// <summary>
    /// Incoterm
    /// </summary>
    public int? InvoiceIncotermId { get; set; }

    /// <summary>
    /// Cash Rounding Method
    /// </summary>
    public int? InvoiceCashRoundingId { get; set; }

    /// <summary>
    /// Created by
    /// </summary>
    public int? CreateUid { get; set; }

    /// <summary>
    /// Last Updated by
    /// </summary>
    public int? WriteUid { get; set; }

    /// <summary>
    /// Sequence Prefix
    /// </summary>
    public string? SequencePrefix { get; set; }

    /// <summary>
    /// Security Token
    /// </summary>
    public string? AccessToken { get; set; }

    /// <summary>
    /// Number
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Reference
    /// </summary>
    public string? Ref { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    public string State { get; set; } = null!;

    /// <summary>
    /// Type
    /// </summary>
    public string MoveType { get; set; } = null!;

    /// <summary>
    /// Auto-post
    /// </summary>
    public string AutoPost { get; set; } = null!;

    /// <summary>
    /// Inalterability Hash
    /// </summary>
    public string? InalterableHash { get; set; }

    /// <summary>
    /// Payment Reference
    /// </summary>
    public string? PaymentReference { get; set; }

    /// <summary>
    /// Payment QR-code
    /// </summary>
    public string? QrCodeMethod { get; set; }

    /// <summary>
    /// Payment Status
    /// </summary>
    public string? PaymentState { get; set; }

    /// <summary>
    /// Source Email
    /// </summary>
    public string? InvoiceSourceEmail { get; set; }

    /// <summary>
    /// Invoice Partner Display Name
    /// </summary>
    public string? InvoicePartnerDisplayName { get; set; }

    /// <summary>
    /// Origin
    /// </summary>
    public string? InvoiceOrigin { get; set; }

    /// <summary>
    /// Incoterm Location
    /// </summary>
    public string? IncotermLocation { get; set; }

    /// <summary>
    /// Date
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// Auto-post until
    /// </summary>
    public DateOnly? AutoPostUntil { get; set; }

    /// <summary>
    /// Invoice/Bill Date
    /// </summary>
    public DateOnly? InvoiceDate { get; set; }

    /// <summary>
    /// Due Date
    /// </summary>
    public DateOnly? InvoiceDateDue { get; set; }

    /// <summary>
    /// Delivery Date
    /// </summary>
    public DateOnly? DeliveryDate { get; set; }

    /// <summary>
    /// Send And Print Values
    /// </summary>
    public string? SendAndPrintValues { get; set; }

    /// <summary>
    /// Terms and Conditions
    /// </summary>
    public string? Narration { get; set; }

    /// <summary>
    /// Untaxed Amount
    /// </summary>
    public decimal? AmountUntaxed { get; set; }

    /// <summary>
    /// Tax
    /// </summary>
    public decimal? AmountTax { get; set; }

    /// <summary>
    /// Total
    /// </summary>
    public decimal? AmountTotal { get; set; }

    /// <summary>
    /// Amount Due
    /// </summary>
    public decimal? AmountResidual { get; set; }

    /// <summary>
    /// Untaxed Amount Signed
    /// </summary>
    public decimal? AmountUntaxedSigned { get; set; }

    /// <summary>
    /// Tax Signed
    /// </summary>
    public decimal? AmountTaxSigned { get; set; }

    /// <summary>
    /// Total Signed
    /// </summary>
    public decimal? AmountTotalSigned { get; set; }

    /// <summary>
    /// Total in Currency Signed
    /// </summary>
    public decimal? AmountTotalInCurrencySigned { get; set; }

    /// <summary>
    /// Amount Due Signed
    /// </summary>
    public decimal? AmountResidualSigned { get; set; }

    /// <summary>
    /// Total (Tax inc.)
    /// </summary>
    public decimal? QuickEditTotalAmount { get; set; }

    /// <summary>
    /// Is Storno
    /// </summary>
    public bool? IsStorno { get; set; }

    /// <summary>
    /// Always Tax Exigible
    /// </summary>
    public bool? AlwaysTaxExigible { get; set; }

    /// <summary>
    /// To Check
    /// </summary>
    public bool? ToCheck { get; set; }

    /// <summary>
    /// Posted Before
    /// </summary>
    public bool? PostedBefore { get; set; }

    /// <summary>
    /// Is Move Sent
    /// </summary>
    public bool? IsMoveSent { get; set; }

    /// <summary>
    /// Created on
    /// </summary>
    public DateTime? CreateDate { get; set; }

    /// <summary>
    /// Last Updated on
    /// </summary>
    public DateTime? WriteDate { get; set; }

    /// <summary>
    /// Payment State Before Switch
    /// </summary>
    public string? PaymentStateBeforeSwitch { get; set; }

    /// <summary>
    /// Originating Model
    /// </summary>
    public int? TransferModelId { get; set; }

    /// <summary>
    /// Stock Move
    /// </summary>
    public int? StockMoveId { get; set; }

    /// <summary>
    /// Tax Closing End Date
    /// </summary>
    public DateOnly? TaxClosingEndDate { get; set; }

    /// <summary>
    /// Tax Report Control Error
    /// </summary>
    public bool? TaxReportControlError { get; set; }

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
    /// Sales Team
    /// </summary>
    public int? TeamId { get; set; }

    /// <summary>
    /// Asset
    /// </summary>
    public int? AssetId { get; set; }

    /// <summary>
    /// Number of days
    /// </summary>
    public int? AssetNumberDays { get; set; }

    /// <summary>
    /// Date of the beginning of the depreciation
    /// </summary>
    public DateOnly? AssetDepreciationBeginningDate { get; set; }

    /// <summary>
    /// Depreciation
    /// </summary>
    public decimal? DepreciationValue { get; set; }

    /// <summary>
    /// Asset Value Change
    /// </summary>
    public bool? AssetValueChange { get; set; }

    /// <summary>
    /// Analytic Account
    /// </summary>
    public int? AnalyticAccountId { get; set; }

    /// <summary>
    /// Failed
    /// </summary>
    public int? Failed { get; set; }

    /// <summary>
    /// Related Receipt Payment Register
    /// </summary>
    public int? ReceiptPaymentId { get; set; }

    /// <summary>
    /// Related Customer Payment Fee Register
    /// </summary>
    public int? CustomerPaymentFeeId { get; set; }

    /// <summary>
    /// Filename
    /// </summary>
    public string? FileName { get; set; }

    /// <summary>
    /// Log Failed
    /// </summary>
    public string? LogFailed { get; set; }

    /// <summary>
    /// Currency Rate
    /// </summary>
    public decimal? CurrencyRate { get; set; }

    /// <summary>
    /// Dummy Untaxed Amount Signed (Positive Value)
    /// </summary>
    public decimal? AmountUntaxedPositive { get; set; }

    /// <summary>
    /// Dummy Total (Positive Value)
    /// </summary>
    public decimal? AmountTotalPositive { get; set; }

    /// <summary>
    /// Dummy Amount Due (Positive Value)
    /// </summary>
    public decimal? AmountResidualPositive { get; set; }

    /// <summary>
    /// Is import invoice ?
    /// </summary>
    public bool? IsImportInvoice { get; set; }

    /// <summary>
    /// Skip Periodical Cost Computation Constraint
    /// </summary>
    public bool? SkipStockConstraint { get; set; }

    /// <summary>
    /// Depreciable Value
    /// </summary>
    public decimal? AssetRemainingValue { get; set; }

    /// <summary>
    /// Cumulative Depreciation
    /// </summary>
    public decimal? AssetDepreciatedValue { get; set; }

    /// <summary>
    /// Asset Value
    /// </summary>
    public decimal? AssetValue { get; set; }

    /// <summary>
    /// Is Asset Dispose Entry
    /// </summary>
    public bool? IsAssetDisposeEntry { get; set; }

    /// <summary>
    /// Is Asset Pause Entry
    /// </summary>
    public bool? IsAssetPauseEntry { get; set; }

    /// <summary>
    /// Regularization
    /// </summary>
    public int? RegularizationId { get; set; }

    /// <summary>
    /// Delivery Note
    /// </summary>
    public bool? DeliveryNote { get; set; }

    /// <summary>
    /// Total Amount Discount
    /// </summary>
    public double? TotalAmountDiscount { get; set; }

    /// <summary>
    /// Fixed Amount Discount
    /// </summary>
    public double? FixedAmountDiscount { get; set; }

    /// <summary>
    /// Vat Partner
    /// </summary>
    public int? VatPartnerId { get; set; }

    /// <summary>
    /// Passed API
    /// </summary>
    public bool? IsNeedApi { get; set; }

    /// <summary>
    /// Sale Order
    /// </summary>
    public int? SaleId { get; set; }

    /// <summary>
    /// Is Downpayment
    /// </summary>
    public bool? IsDownpayment { get; set; }

    /// <summary>
    /// Total Fix Amount Discount
    /// </summary>
    public double? TotalFixAmountDiscount { get; set; }

    /// <summary>
    /// Total Discount (%)
    /// </summary>
    public double? TotalDiscount { get; set; }

    /// <summary>
    /// Confirm Method
    /// </summary>
    public int? ConfirmMethod { get; set; }

    /// <summary>
    /// Expense Sheet
    /// </summary>
    public int? ExpenseSheetId { get; set; }

    /// <summary>
    /// Delivery Order
    /// </summary>
    public string? DeliveryOrder { get; set; }

    /// <summary>
    /// SO Type
    /// </summary>
    public int? SoTypeId { get; set; }

    /// <summary>
    /// Expense
    /// </summary>
    public int? ExpenseId { get; set; }

    /// <summary>
    /// Raw Data ID
    /// </summary>
    public int? RawDataId { get; set; }

    /// <summary>
    /// Vendor Tax Code
    /// </summary>
    public string? VendorTaxCode { get; set; }

    /// <summary>
    /// Source Document
    /// </summary>
    public string? SourceDocument { get; set; }

    /// <summary>
    /// Invoice Reference
    /// </summary>
    public string? InvoiceReference { get; set; }

    /// <summary>
    /// Invoice Number
    /// </summary>
    public string? InvoiceNumber { get; set; }

    /// <summary>
    /// Source Invoice
    /// </summary>
    public int? AutoInvoiceId { get; set; }

    /// <summary>
    /// Auto Generated Document
    /// </summary>
    public bool? AutoGenerated { get; set; }

    /// <summary>
    /// Company Dest
    /// </summary>
    public int? DestCompanyId { get; set; }

    /// <summary>
    /// Is Inter-company Transaction ?
    /// </summary>
    public bool? IsInterCompanyTransaction { get; set; }

    /// <summary>
    /// Issue VAT Invoice Count
    /// </summary>
    public int? IssueCount { get; set; }

    /// <summary>
    /// VAT Partner
    /// </summary>
    public int? PartnerInvoiceId { get; set; }

    /// <summary>
    /// Buyer Name on VAT Invoice
    /// </summary>
    public string? BuyerNameVatInvoice { get; set; }

    /// <summary>
    /// Issue Status
    /// </summary>
    public string? IssueState { get; set; }

    /// <summary>
    /// VAT Invoice Status
    /// </summary>
    public string? VatInvoiceStatus { get; set; }

    /// <summary>
    /// VAT Invoice Type
    /// </summary>
    public string? VatInvoiceType { get; set; }

    /// <summary>
    /// Issue VAT Date
    /// </summary>
    public DateOnly? IssueVatDate { get; set; }

    /// <summary>
    /// Need Issue VAT
    /// </summary>
    public bool? IsNeedIssueVat { get; set; }

    /// <summary>
    /// Issued VAT
    /// </summary>
    public bool? IsIssuedVat { get; set; }

    /// <summary>
    /// Platform?
    /// </summary>
    public bool? Platform { get; set; }

    public virtual ICollection<AccountPayment> AccountPayments { get; set; } = new List<AccountPayment>();

    public virtual AccountAnalyticAccount? AnalyticAccount { get; set; }

    public virtual AccountMove? AutoInvoice { get; set; }

    public virtual AccountMove? AutoPostOrigin { get; set; }

    public virtual ResPartner? CommercialPartner { get; set; }

    public virtual ResUser? CreateU { get; set; }

    public virtual AccountPayment? CustomerPaymentFee { get; set; }

    public virtual ICollection<AccountMove> InverseAutoInvoice { get; set; } = new List<AccountMove>();

    public virtual ICollection<AccountMove> InverseAutoPostOrigin { get; set; } = new List<AccountMove>();

    public virtual ICollection<AccountMove> InverseReversedEntry { get; set; } = new List<AccountMove>();

    public virtual ICollection<AccountMove> InverseTaxCashBasisOriginMove { get; set; } = new List<AccountMove>();

    public virtual ResUser? InvoiceUser { get; set; }

    public virtual AccountJournal Journal { get; set; } = null!;

    public virtual ResPartner? Partner { get; set; }

    public virtual ResPartner? PartnerInvoice { get; set; }

    public virtual ResPartner? PartnerShipping { get; set; }

    public virtual AccountPayment? Payment { get; set; }

    public virtual AccountPayment? ReceiptPayment { get; set; }

    public virtual AccountMove? ReversedEntry { get; set; }

    public virtual SaleOrder? Sale { get; set; }

    public virtual AccountMove? TaxCashBasisOriginMove { get; set; }

    public virtual ResPartner? VatPartner { get; set; }

    public virtual ResUser? WriteU { get; set; }
}
