using System;
using System.Collections.Generic;

namespace Odoo.Domain.Entities;

/// <summary>
/// Analytic Account
/// </summary>
public partial class AccountAnalyticAccount
{
    public int Id { get; set; }

    /// <summary>
    /// Plan
    /// </summary>
    public int? PlanId { get; set; }

    /// <summary>
    /// Root Plan
    /// </summary>
    public int? RootPlanId { get; set; }

    /// <summary>
    /// Company
    /// </summary>
    public int? CompanyId { get; set; }

    /// <summary>
    /// Customer
    /// </summary>
    public int? PartnerId { get; set; }

    /// <summary>
    /// Created by
    /// </summary>
    public int? CreateUid { get; set; }

    /// <summary>
    /// Last Updated by
    /// </summary>
    public int? WriteUid { get; set; }

    /// <summary>
    /// Reference
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// Analytic Account
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Active
    /// </summary>
    public bool? Active { get; set; }

    /// <summary>
    /// Created on
    /// </summary>
    public DateTime? CreateDate { get; set; }

    /// <summary>
    /// Last Updated on
    /// </summary>
    public DateTime? WriteDate { get; set; }

    public string? ParentPath { get; set; }

    /// <summary>
    /// Parent Analytic Account
    /// </summary>
    public int? ParentId { get; set; }

    /// <summary>
    /// Level
    /// </summary>
    public int? Level { get; set; }

    /// <summary>
    /// Company Analytic Account
    /// </summary>
    public int? CompanyAnalyticAccountId { get; set; }

    /// <summary>
    /// Full Analytic Hierarchy Name
    /// </summary>
    public string? CompleteName { get; set; }

    /// <summary>
    /// Analytic Type
    /// </summary>
    public string? AnalyticType { get; set; }

    /// <summary>
    /// Will API Consignment
    /// </summary>
    public bool? WillApiConsignment { get; set; }

    /// <summary>
    /// Will API Warehouse Transfer
    /// </summary>
    public bool? WillApiWarehouseTransfer { get; set; }

    /// <summary>
    /// Synchronize ID
    /// </summary>
    public int? SyncId { get; set; }

    public virtual ICollection<AccountMove> AccountMoves { get; set; } = new List<AccountMove>();

    public virtual ICollection<AccountPayment> AccountPayments { get; set; } = new List<AccountPayment>();

    public virtual AccountAnalyticAccount? CompanyAnalyticAccount { get; set; }

    public virtual ResUser? CreateU { get; set; }

    public virtual ICollection<AccountAnalyticAccount> InverseCompanyAnalyticAccount { get; set; } = new List<AccountAnalyticAccount>();

    public virtual ICollection<AccountAnalyticAccount> InverseParent { get; set; } = new List<AccountAnalyticAccount>();

    public virtual AccountAnalyticAccount? Parent { get; set; }

    public virtual ResPartner? Partner { get; set; }

    public virtual ICollection<SaleOrder> SaleOrders { get; set; } = new List<SaleOrder>();

    public virtual ResUser? WriteU { get; set; }
}
