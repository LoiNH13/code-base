using System;
using System.Collections.Generic;

namespace Odoo.Domain.Entities;

public partial class ResUser
{
    public int Id { get; set; }

    public int CompanyId { get; set; }

    public int PartnerId { get; set; }

    public bool? Active { get; set; }

    public DateTime? CreateDate { get; set; }

    public string Login { get; set; } = null!;

    public string? Password { get; set; }

    /// <summary>
    /// Home Action
    /// </summary>
    public int? ActionId { get; set; }

    /// <summary>
    /// Created by
    /// </summary>
    public int? CreateUid { get; set; }

    /// <summary>
    /// Last Updated by
    /// </summary>
    public int? WriteUid { get; set; }

    /// <summary>
    /// Email Signature
    /// </summary>
    public string? Signature { get; set; }

    /// <summary>
    /// Share User
    /// </summary>
    public bool? Share { get; set; }

    /// <summary>
    /// Last Updated on
    /// </summary>
    public DateTime? WriteDate { get; set; }

    public string? TotpSecret { get; set; }

    /// <summary>
    /// Notification
    /// </summary>
    public string NotificationType { get; set; } = null!;

    /// <summary>
    /// OdooBot Status
    /// </summary>
    public string? OdoobotState { get; set; }

    /// <summary>
    /// Odoobot Failed
    /// </summary>
    public bool? OdoobotFailed { get; set; }

    /// <summary>
    /// User Sales Team
    /// </summary>
    public int? SaleTeamId { get; set; }

    /// <summary>
    /// Warehouses Dom
    /// </summary>
    public string? WarehousesDom { get; set; }

    /// <summary>
    /// Warehouses Allow Dom
    /// </summary>
    public string? WarehousesAllowDom { get; set; }

    /// <summary>
    /// Working All Warehouse
    /// </summary>
    public bool? WorkingAllWarehouse { get; set; }

    /// <summary>
    /// Follow All Warehouse
    /// </summary>
    public bool? FollowAllWarehouse { get; set; }

    /// <summary>
    /// Won in Opportunities Target
    /// </summary>
    public int? TargetSalesWon { get; set; }

    /// <summary>
    /// Activities Done Target
    /// </summary>
    public int? TargetSalesDone { get; set; }

    /// <summary>
    /// Invoiced in Sales Orders Target
    /// </summary>
    public int? TargetSalesInvoiced { get; set; }

    /// <summary>
    /// Is Sales User ?
    /// </summary>
    public bool? SaleUser { get; set; }

    /// <summary>
    /// OAuth Provider
    /// </summary>
    public int? OauthProviderId { get; set; }

    /// <summary>
    /// OAuth User ID
    /// </summary>
    public string? OauthUid { get; set; }

    /// <summary>
    /// OAuth Access Token
    /// </summary>
    public string? OauthAccessToken { get; set; }

    /// <summary>
    /// Karma
    /// </summary>
    public int? Karma { get; set; }

    /// <summary>
    /// Rank
    /// </summary>
    public int? RankId { get; set; }

    /// <summary>
    /// Next Rank
    /// </summary>
    public int? NextRankId { get; set; }

    public virtual ICollection<AccountAnalyticAccount> AccountAnalyticAccountCreateUs { get; set; } = new List<AccountAnalyticAccount>();

    public virtual ICollection<AccountAnalyticAccount> AccountAnalyticAccountWriteUs { get; set; } = new List<AccountAnalyticAccount>();

    public virtual ICollection<AccountJournal> AccountJournalCreateUs { get; set; } = new List<AccountJournal>();

    public virtual ICollection<AccountJournal> AccountJournalSaleActivityUsers { get; set; } = new List<AccountJournal>();

    public virtual ICollection<AccountJournal> AccountJournalWriteUs { get; set; } = new List<AccountJournal>();

    public virtual ICollection<AccountMove> AccountMoveCreateUs { get; set; } = new List<AccountMove>();

    public virtual ICollection<AccountMove> AccountMoveInvoiceUsers { get; set; } = new List<AccountMove>();

    public virtual ICollection<AccountMove> AccountMoveWriteUs { get; set; } = new List<AccountMove>();

    public virtual ICollection<AccountPayment> AccountPaymentCreateUs { get; set; } = new List<AccountPayment>();

    public virtual ICollection<AccountPayment> AccountPaymentUsers { get; set; } = new List<AccountPayment>();

    public virtual ICollection<AccountPayment> AccountPaymentWriteUs { get; set; } = new List<AccountPayment>();

    public virtual ResUser? CreateU { get; set; }

    public virtual ICollection<ResUser> InverseCreateU { get; set; } = new List<ResUser>();

    public virtual ICollection<ResUser> InverseWriteU { get; set; } = new List<ResUser>();

    public virtual ResPartner Partner { get; set; } = null!;

    public virtual ICollection<ProductCategory> ProductCategoryCreateUs { get; set; } = new List<ProductCategory>();

    public virtual ICollection<ProductCategory> ProductCategoryWriteUs { get; set; } = new List<ProductCategory>();

    public virtual ICollection<ProductProduct> ProductProductCreateUs { get; set; } = new List<ProductProduct>();

    public virtual ICollection<ProductProduct> ProductProductWriteUs { get; set; } = new List<ProductProduct>();

    public virtual ICollection<ResCountryState> ResCountryStateCreateUs { get; set; } = new List<ResCountryState>();

    public virtual ICollection<ResCountryState> ResCountryStateWriteUs { get; set; } = new List<ResCountryState>();

    public virtual ICollection<ResDistrict> ResDistrictCreateUs { get; set; } = new List<ResDistrict>();

    public virtual ICollection<ResDistrict> ResDistrictWriteUs { get; set; } = new List<ResDistrict>();

    public virtual ICollection<ResPartner> ResPartnerBuyers { get; set; } = new List<ResPartner>();

    public virtual ICollection<ResPartner> ResPartnerCreateUs { get; set; } = new List<ResPartner>();

    public virtual ICollection<ResPartner> ResPartnerUsers { get; set; } = new List<ResPartner>();

    public virtual ICollection<ResPartner> ResPartnerWriteUs { get; set; } = new List<ResPartner>();

    public virtual ICollection<ResWard> ResWardCreateUs { get; set; } = new List<ResWard>();

    public virtual ICollection<ResWard> ResWardWriteUs { get; set; } = new List<ResWard>();

    public virtual ICollection<ResourceResource> ResourceResourceCreateUs { get; set; } = new List<ResourceResource>();

    public virtual ICollection<ResourceResource> ResourceResourceUsers { get; set; } = new List<ResourceResource>();

    public virtual ICollection<ResourceResource> ResourceResourceWriteUs { get; set; } = new List<ResourceResource>();

    public virtual ICollection<SaleOrder> SaleOrderCreateUs { get; set; } = new List<SaleOrder>();

    public virtual ICollection<SaleOrder> SaleOrderFirstApproverRecords { get; set; } = new List<SaleOrder>();

    public virtual ICollection<SaleOrder> SaleOrderSecondApproverRecords { get; set; } = new List<SaleOrder>();

    public virtual ICollection<SaleOrder> SaleOrderUsers { get; set; } = new List<SaleOrder>();

    public virtual ICollection<SaleOrder> SaleOrderWriteUs { get; set; } = new List<SaleOrder>();

    public virtual ICollection<StockLot> StockLotCreateUs { get; set; } = new List<StockLot>();

    public virtual ICollection<StockLot> StockLotSalespeople { get; set; } = new List<StockLot>();

    public virtual ICollection<StockLot> StockLotWriteUs { get; set; } = new List<StockLot>();

    public virtual ResUser? WriteU { get; set; }
}
