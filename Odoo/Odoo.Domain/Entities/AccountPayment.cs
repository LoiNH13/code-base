using System;
using System.Collections.Generic;

namespace Odoo.Domain.Entities;

/// <summary>
/// Payments
/// </summary>
public partial class AccountPayment
{
    public int Id { get; set; }

    /// <summary>
    /// Main Attachment
    /// </summary>
    public int? MessageMainAttachmentId { get; set; }

    /// <summary>
    /// Journal Entry
    /// </summary>
    public int MoveId { get; set; }

    /// <summary>
    /// Recipient Bank Account
    /// </summary>
    public int? PartnerBankId { get; set; }

    /// <summary>
    /// Paired Internal Transfer Payment
    /// </summary>
    public int? PairedInternalTransferPaymentId { get; set; }

    /// <summary>
    /// Payment Method
    /// </summary>
    public int? PaymentMethodLineId { get; set; }

    /// <summary>
    /// Method
    /// </summary>
    public int? PaymentMethodId { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    public int CurrencyId { get; set; }

    /// <summary>
    /// Customer/Vendor
    /// </summary>
    public int? PartnerId { get; set; }

    /// <summary>
    /// Outstanding Account
    /// </summary>
    public int? OutstandingAccountId { get; set; }

    /// <summary>
    /// Destination Account
    /// </summary>
    public int? DestinationAccountId { get; set; }

    /// <summary>
    /// Destination Journal
    /// </summary>
    public int? DestinationJournalId { get; set; }

    /// <summary>
    /// Created by
    /// </summary>
    public int? CreateUid { get; set; }

    /// <summary>
    /// Last Updated by
    /// </summary>
    public int? WriteUid { get; set; }

    /// <summary>
    /// Payment Type
    /// </summary>
    public string PaymentType { get; set; } = null!;

    /// <summary>
    /// Partner Type
    /// </summary>
    public string PartnerType { get; set; } = null!;

    /// <summary>
    /// Payment Reference
    /// </summary>
    public string? PaymentReference { get; set; }

    /// <summary>
    /// Amount
    /// </summary>
    public decimal? Amount { get; set; }

    /// <summary>
    /// Amount Company Currency Signed
    /// </summary>
    public decimal? AmountCompanyCurrencySigned { get; set; }

    /// <summary>
    /// Is Reconciled
    /// </summary>
    public bool? IsReconciled { get; set; }

    /// <summary>
    /// Is Matched With a Bank Statement
    /// </summary>
    public bool? IsMatched { get; set; }

    /// <summary>
    /// Internal Transfer
    /// </summary>
    public bool? IsInternalTransfer { get; set; }

    /// <summary>
    /// Created on
    /// </summary>
    public DateTime? CreateDate { get; set; }

    /// <summary>
    /// Last Updated on
    /// </summary>
    public DateTime? WriteDate { get; set; }

    /// <summary>
    /// Payment Transaction
    /// </summary>
    public int? PaymentTransactionId { get; set; }

    /// <summary>
    /// Saved Payment Token
    /// </summary>
    public int? PaymentTokenId { get; set; }

    /// <summary>
    /// Source Payment
    /// </summary>
    public int? SourcePaymentId { get; set; }

    /// <summary>
    /// Difference Account
    /// </summary>
    public int? WriteoffAccountId { get; set; }

    /// <summary>
    /// Responsible
    /// </summary>
    public string? Responsible { get; set; }

    /// <summary>
    /// Payment Difference Handling
    /// </summary>
    public string? PaymentDifferenceHandling { get; set; }

    /// <summary>
    /// Journal Item Label
    /// </summary>
    public string? WriteoffLabel { get; set; }

    /// <summary>
    /// Realized Gain Loss Entry Date
    /// </summary>
    public DateOnly? RealizedGainLossDate { get; set; }

    /// <summary>
    /// Amount Converted at Payment Rate
    /// </summary>
    public decimal? ConvertedAmountAtPaymentRate { get; set; }

    /// <summary>
    /// Amount Converted at Invoice Rate
    /// </summary>
    public decimal? ConvertedAmountAtInvoiceRate { get; set; }

    /// <summary>
    /// Realized Gain Loss Amount
    /// </summary>
    public decimal? RealizedGainLossAmount { get; set; }

    /// <summary>
    /// Total Amount (Inc. Payment Receipts)
    /// </summary>
    public decimal? TotalPayment { get; set; }

    /// <summary>
    /// Conversion Amount
    /// </summary>
    public decimal? ConversionCurrencyAmount { get; set; }

    /// <summary>
    /// Converted Total Amount (Inc. Payment Receipts)
    /// </summary>
    public decimal? TotalPaymentCurrency { get; set; }

    /// <summary>
    /// Payment Difference
    /// </summary>
    public decimal? PaymentDifference { get; set; }

    /// <summary>
    /// Show Payment Receipts
    /// </summary>
    public bool? ShowInvoice { get; set; }

    /// <summary>
    /// Related Partner Reconciliation
    /// </summary>
    public int? RelatedPartnerReconciliationId { get; set; }

    /// <summary>
    /// Send/ Receive Person
    /// </summary>
    public string? SendReceivePerson { get; set; }

    /// <summary>
    /// Cashback Payable Account
    /// </summary>
    public int? CashbackPayableAccountId { get; set; }

    /// <summary>
    /// Internal Reference
    /// </summary>
    public string? InternalReference { get; set; }

    /// <summary>
    /// Salesperson
    /// </summary>
    public int? UserId { get; set; }

    /// <summary>
    /// Vat Partner
    /// </summary>
    public int? VatPartnerId { get; set; }

    /// <summary>
    /// V15 Type
    /// </summary>
    public string? V15Type { get; set; }

    /// <summary>
    /// Need API
    /// </summary>
    public bool? IsNeedApi { get; set; }

    /// <summary>
    /// Passed API
    /// </summary>
    public bool? IsPassedApi { get; set; }

    /// <summary>
    /// Journal
    /// </summary>
    public int? JournalId { get; set; }

    /// <summary>
    /// Company Currency
    /// </summary>
    public int? CompanyCurrencyId { get; set; }

    /// <summary>
    /// Analytic Account
    /// </summary>
    public int? AnalyticAccountId { get; set; }

    /// <summary>
    /// Currency Rate
    /// </summary>
    public double? CurrencyRate { get; set; }

    /// <summary>
    /// Related Expenses
    /// </summary>
    public int? ExpensesPaymentId { get; set; }

    /// <summary>
    /// PO Request
    /// </summary>
    public int? PoRequestId { get; set; }

    /// <summary>
    /// x_review_result
    /// </summary>
    public string? XReviewResult { get; set; }

    /// <summary>
    /// x_has_request_approval
    /// </summary>
    public bool? XHasRequestApproval { get; set; }

    /// <summary>
    /// Other System Amount
    /// </summary>
    public decimal? SystemAmount { get; set; }

    /// <summary>
    /// Platform?
    /// </summary>
    public bool? Platform { get; set; }

    public virtual ICollection<AccountMove> AccountMoveCustomerPaymentFees { get; set; } = new List<AccountMove>();

    public virtual ICollection<AccountMove> AccountMovePayments { get; set; } = new List<AccountMove>();

    public virtual ICollection<AccountMove> AccountMoveReceiptPayments { get; set; } = new List<AccountMove>();

    public virtual AccountAnalyticAccount? AnalyticAccount { get; set; }

    public virtual ResUser? CreateU { get; set; }

    public virtual AccountJournal? DestinationJournal { get; set; }

    public virtual ICollection<AccountPayment> InversePairedInternalTransferPayment { get; set; } = new List<AccountPayment>();

    public virtual ICollection<AccountPayment> InverseSourcePayment { get; set; } = new List<AccountPayment>();

    public virtual AccountJournal? Journal { get; set; }

    public virtual AccountMove Move { get; set; } = null!;

    public virtual AccountPayment? PairedInternalTransferPayment { get; set; }

    public virtual ResPartner? Partner { get; set; }

    public virtual AccountPayment? SourcePayment { get; set; }

    public virtual ResUser? User { get; set; }

    public virtual ResPartner? VatPartner { get; set; }

    public virtual ResUser? WriteU { get; set; }
}
