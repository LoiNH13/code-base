using System;
using System.Collections.Generic;

namespace Odoo.Domain.Entities;

/// <summary>
/// Journal
/// </summary>
public partial class AccountJournal
{
    public int Id { get; set; }

    /// <summary>
    /// Alias
    /// </summary>
    public int? AliasId { get; set; }

    /// <summary>
    /// Default Account
    /// </summary>
    public int? DefaultAccountId { get; set; }

    /// <summary>
    /// Suspense Account
    /// </summary>
    public int? SuspenseAccountId { get; set; }

    /// <summary>
    /// Sequence
    /// </summary>
    public int? Sequence { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    public int? CurrencyId { get; set; }

    /// <summary>
    /// Company
    /// </summary>
    public int? CompanyId { get; set; }

    /// <summary>
    /// Profit Account
    /// </summary>
    public int? ProfitAccountId { get; set; }

    /// <summary>
    /// Loss Account
    /// </summary>
    public int? LossAccountId { get; set; }

    /// <summary>
    /// Bank Account
    /// </summary>
    public int? BankAccountId { get; set; }

    /// <summary>
    /// Schedule Activity
    /// </summary>
    public int? SaleActivityTypeId { get; set; }

    /// <summary>
    /// Activity User
    /// </summary>
    public int? SaleActivityUserId { get; set; }

    /// <summary>
    /// Secure Sequence
    /// </summary>
    public int? SecureSequenceId { get; set; }

    /// <summary>
    /// Created by
    /// </summary>
    public int? CreateUid { get; set; }

    /// <summary>
    /// Last Updated by
    /// </summary>
    public int? WriteUid { get; set; }

    /// <summary>
    /// Color Index
    /// </summary>
    public int? Color { get; set; }

    /// <summary>
    /// Security Token
    /// </summary>
    public string? AccessToken { get; set; }

    /// <summary>
    /// Short Code
    /// </summary>
    public string Code { get; set; } = null!;

    /// <summary>
    /// Type
    /// </summary>
    public string Type { get; set; } = null!;

    /// <summary>
    /// Communication Type
    /// </summary>
    public string InvoiceReferenceType { get; set; } = null!;

    /// <summary>
    /// Communication Standard
    /// </summary>
    public string InvoiceReferenceModel { get; set; } = null!;

    /// <summary>
    /// Bank Feeds
    /// </summary>
    public string? BankStatementsSource { get; set; }

    /// <summary>
    /// Journal Name
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Sequence Override Regex
    /// </summary>
    public string? SequenceOverrideRegex { get; set; }

    /// <summary>
    /// Activity Summary
    /// </summary>
    public string? SaleActivityNote { get; set; }

    /// <summary>
    /// Active
    /// </summary>
    public bool? Active { get; set; }

    /// <summary>
    /// Lock Posted Entries with Hash
    /// </summary>
    public bool? RestrictModeHashTable { get; set; }

    /// <summary>
    /// Dedicated Credit Note Sequence
    /// </summary>
    public bool? RefundSequence { get; set; }

    /// <summary>
    /// Dedicated Payment Sequence
    /// </summary>
    public bool? PaymentSequence { get; set; }

    /// <summary>
    /// Show journal on dashboard
    /// </summary>
    public bool? ShowOnDashboard { get; set; }

    /// <summary>
    /// Created on
    /// </summary>
    public DateTime? CreateDate { get; set; }

    /// <summary>
    /// Last Updated on
    /// </summary>
    public DateTime? WriteDate { get; set; }

    /// <summary>
    /// Account Online Account
    /// </summary>
    public int? AccountOnlineAccountId { get; set; }

    /// <summary>
    /// Account Online Link
    /// </summary>
    public int? AccountOnlineLinkId { get; set; }

    /// <summary>
    /// Connection Requests
    /// </summary>
    public string? RenewalContactEmail { get; set; }

    /// <summary>
    /// Trade Discount Account
    /// </summary>
    public int? AccountDiscountId { get; set; }

    /// <summary>
    /// Entry Sequence
    /// </summary>
    public int? SequenceId { get; set; }

    /// <summary>
    /// Enable Trade Discount
    /// </summary>
    public bool? GenerateDiscountAccount { get; set; }

    /// <summary>
    /// Full API
    /// </summary>
    public bool? IsFullApi { get; set; }

    /// <summary>
    /// Synchronize ID
    /// </summary>
    public int? SyncId { get; set; }

    /// <summary>
    /// Use for Employee Expenses
    /// </summary>
    public bool? UseForEmpExpense { get; set; }

    public virtual ICollection<AccountMove> AccountMoves { get; set; } = new List<AccountMove>();

    public virtual ICollection<AccountPayment> AccountPaymentDestinationJournals { get; set; } = new List<AccountPayment>();

    public virtual ICollection<AccountPayment> AccountPaymentJournals { get; set; } = new List<AccountPayment>();

    public virtual ResUser? CreateU { get; set; }

    public virtual ResUser? SaleActivityUser { get; set; }

    public virtual ICollection<SaleOrder> SaleOrders { get; set; } = new List<SaleOrder>();

    public virtual ResUser? WriteU { get; set; }
}
