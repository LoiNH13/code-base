using System;
using System.Collections.Generic;

namespace Odoo.Domain.Entities;

public partial class ResPartner
{
    public int Id { get; set; }

    public int? CompanyId { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? Name { get; set; }

    /// <summary>
    /// Title
    /// </summary>
    public int? Title { get; set; }

    /// <summary>
    /// Related Company
    /// </summary>
    public int? ParentId { get; set; }

    /// <summary>
    /// Salesperson
    /// </summary>
    public int? UserId { get; set; }

    /// <summary>
    /// State
    /// </summary>
    public int? StateId { get; set; }

    /// <summary>
    /// Country
    /// </summary>
    public int? CountryId { get; set; }

    /// <summary>
    /// Industry
    /// </summary>
    public int? IndustryId { get; set; }

    /// <summary>
    /// Color Index
    /// </summary>
    public int? Color { get; set; }

    /// <summary>
    /// Commercial Entity
    /// </summary>
    public int? CommercialPartnerId { get; set; }

    /// <summary>
    /// Created by
    /// </summary>
    public int? CreateUid { get; set; }

    /// <summary>
    /// Last Updated by
    /// </summary>
    public int? WriteUid { get; set; }

    /// <summary>
    /// Complete Name
    /// </summary>
    public string? CompleteName { get; set; }

    /// <summary>
    /// Reference
    /// </summary>
    public string? Ref { get; set; }

    /// <summary>
    /// Language
    /// </summary>
    public string? Lang { get; set; }

    /// <summary>
    /// Timezone
    /// </summary>
    public string? Tz { get; set; }

    /// <summary>
    /// Tax ID
    /// </summary>
    public string? Vat { get; set; }

    /// <summary>
    /// Company ID
    /// </summary>
    public string? CompanyRegistry { get; set; }

    /// <summary>
    /// Website Link
    /// </summary>
    public string? Website { get; set; }

    /// <summary>
    /// Job Position
    /// </summary>
    public string? Function { get; set; }

    /// <summary>
    /// Address Type
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// Street
    /// </summary>
    public string? Street { get; set; }

    /// <summary>
    /// Street2
    /// </summary>
    public string? Street2 { get; set; }

    /// <summary>
    /// Zip
    /// </summary>
    public string? Zip { get; set; }

    /// <summary>
    /// City
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// Email
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Phone
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// Mobile
    /// </summary>
    public string? Mobile { get; set; }

    /// <summary>
    /// Company Name Entity
    /// </summary>
    public string? CommercialCompanyName { get; set; }

    /// <summary>
    /// Company Name
    /// </summary>
    public string? CompanyName { get; set; }

    /// <summary>
    /// Date
    /// </summary>
    public DateOnly? Date { get; set; }

    /// <summary>
    /// Notes
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    /// Geo Latitude
    /// </summary>
    public decimal? PartnerLatitude { get; set; }

    /// <summary>
    /// Geo Longitude
    /// </summary>
    public decimal? PartnerLongitude { get; set; }

    /// <summary>
    /// Active
    /// </summary>
    public bool? Active { get; set; }

    /// <summary>
    /// Employee
    /// </summary>
    public bool? Employee { get; set; }

    /// <summary>
    /// Is a Company
    /// </summary>
    public bool? IsCompany { get; set; }

    /// <summary>
    /// Share Partner
    /// </summary>
    public bool? PartnerShare { get; set; }

    /// <summary>
    /// Last Updated on
    /// </summary>
    public DateTime? WriteDate { get; set; }

    /// <summary>
    /// Contact Address Complete
    /// </summary>
    public string? ContactAddressComplete { get; set; }

    /// <summary>
    /// Bounce
    /// </summary>
    public int? MessageBounce { get; set; }

    /// <summary>
    /// Normalized Email
    /// </summary>
    public string? EmailNormalized { get; set; }

    /// <summary>
    /// Signup Token Type
    /// </summary>
    public string? SignupType { get; set; }

    /// <summary>
    /// Signup Expiration
    /// </summary>
    public DateTime? SignupExpiration { get; set; }

    public string? SignupToken { get; set; }

    /// <summary>
    /// Sanitized Number
    /// </summary>
    public string? PhoneSanitized { get; set; }

    /// <summary>
    /// OCN Token
    /// </summary>
    public string? OcnToken { get; set; }

    /// <summary>
    /// Supplier Rank
    /// </summary>
    public int? SupplierRank { get; set; }

    /// <summary>
    /// Customer Rank
    /// </summary>
    public int? CustomerRank { get; set; }

    /// <summary>
    /// Invoice
    /// </summary>
    public string? InvoiceWarn { get; set; }

    /// <summary>
    /// Message for Invoice
    /// </summary>
    public string? InvoiceWarnMsg { get; set; }

    /// <summary>
    /// Payable Limit
    /// </summary>
    public decimal? DebitLimit { get; set; }

    /// <summary>
    /// Latest Invoices &amp; Payments Matching Date
    /// </summary>
    public DateTime? LastTimeEntriesChecked { get; set; }

    /// <summary>
    /// Format
    /// </summary>
    public string? UblCiiFormat { get; set; }

    /// <summary>
    /// Peppol Endpoint
    /// </summary>
    public string? PeppolEndpoint { get; set; }

    /// <summary>
    /// Peppol e-address (EAS)
    /// </summary>
    public string? PeppolEas { get; set; }

    /// <summary>
    /// Online Partner Information
    /// </summary>
    public string? OnlinePartnerInformation { get; set; }

    /// <summary>
    /// Sales Team
    /// </summary>
    public int? TeamId { get; set; }

    /// <summary>
    /// Stock Picking
    /// </summary>
    public string? PickingWarn { get; set; }

    /// <summary>
    /// Message for Stock Picking
    /// </summary>
    public string? PickingWarnMsg { get; set; }

    /// <summary>
    /// Buyer
    /// </summary>
    public int? BuyerId { get; set; }

    /// <summary>
    /// Purchase Order
    /// </summary>
    public string? PurchaseWarn { get; set; }

    /// <summary>
    /// Message for Purchase Order
    /// </summary>
    public string? PurchaseWarnMsg { get; set; }

    /// <summary>
    /// Ward
    /// </summary>
    public int? WardId { get; set; }

    /// <summary>
    /// District
    /// </summary>
    public int? DistrictId { get; set; }

    /// <summary>
    /// Customer
    /// </summary>
    public bool? IsCustomer { get; set; }

    /// <summary>
    /// Vendor
    /// </summary>
    public bool? IsVendor { get; set; }

    /// <summary>
    /// Sales Warnings
    /// </summary>
    public string? SaleWarn { get; set; }

    /// <summary>
    /// Message for Sales Order
    /// </summary>
    public string? SaleWarnMsg { get; set; }

    /// <summary>
    /// Reminders
    /// </summary>
    public string? FollowupReminderType { get; set; }

    /// <summary>
    /// Last notification marked as read from base Calendar
    /// </summary>
    public DateTime? CalendarLastNotifAck { get; set; }

    /// <summary>
    /// Res Partner Group
    /// </summary>
    public int? PartnerGroupId { get; set; }

    /// <summary>
    /// Scale
    /// </summary>
    public int? Scale { get; set; }

    /// <summary>
    /// Car Company
    /// </summary>
    public int? CarCompanyIds { get; set; }

    /// <summary>
    /// Shipping Type
    /// </summary>
    public int? ShippingTypeIds { get; set; }

    /// <summary>
    /// Pricelist
    /// </summary>
    public int? PricelistIdVal { get; set; }

    /// <summary>
    /// VAT Name
    /// </summary>
    public string? VatName { get; set; }

    /// <summary>
    /// V15 Code
    /// </summary>
    public string? V15Code { get; set; }

    /// <summary>
    /// Login
    /// </summary>
    public string? Login { get; set; }

    /// <summary>
    /// Business registration
    /// </summary>
    public string? BusinessRegistrations { get; set; }

    /// <summary>
    /// Reference
    /// </summary>
    public string? Reference { get; set; }

    /// <summary>
    /// Identification No
    /// </summary>
    public string? IdentificationId { get; set; }

    /// <summary>
    /// Warranty Type
    /// </summary>
    public string? WarrantyType { get; set; }

    /// <summary>
    /// Gender
    /// </summary>
    public string? Gender { get; set; }

    /// <summary>
    /// Agency&apos;s Name
    /// </summary>
    public string? AgencyName { get; set; }

    /// <summary>
    /// Agency&apos;s Mobile
    /// </summary>
    public string? AgencyMobile { get; set; }

    /// <summary>
    /// Agency&apos;s email
    /// </summary>
    public string? AgencyEmail { get; set; }

    /// <summary>
    /// Buyer&apos;s Name
    /// </summary>
    public string? BuyerName { get; set; }

    /// <summary>
    /// Buyer&apos;s Mobile
    /// </summary>
    public string? BuyerMobile { get; set; }

    /// <summary>
    /// Buyer&apos;s Email
    /// </summary>
    public string? BuyerEmail { get; set; }

    /// <summary>
    /// Payer&apos;s Name
    /// </summary>
    public string? PayerName { get; set; }

    /// <summary>
    /// Payer&apos;s Mobile
    /// </summary>
    public string? PayerMobile { get; set; }

    /// <summary>
    /// Payer&apos;s Email
    /// </summary>
    public string? PayerEmail { get; set; }

    /// <summary>
    /// Shipping Address
    /// </summary>
    public string? ShippingAddress { get; set; }

    /// <summary>
    /// Pricelist
    /// </summary>
    public string? PricelistName { get; set; }

    /// <summary>
    /// Date of Birth
    /// </summary>
    public DateOnly? Birthday { get; set; }

    /// <summary>
    /// Is need API
    /// </summary>
    public bool? IsNeedApi { get; set; }

    /// <summary>
    /// Opt-Out
    /// </summary>
    public bool? OptOut { get; set; }

    /// <summary>
    /// Channel
    /// </summary>
    public int? ChannelId { get; set; }

    /// <summary>
    /// Sub Channel
    /// </summary>
    public int? SubChannelId { get; set; }

    /// <summary>
    /// Region
    /// </summary>
    public int? RegionId { get; set; }

    /// <summary>
    /// Area
    /// </summary>
    public int? AreaId { get; set; }

    /// <summary>
    /// Debit Limit
    /// </summary>
    public double? DebitLimitAmount { get; set; }

    /// <summary>
    /// Credit Limit
    /// </summary>
    public double? CreditLimitAmount { get; set; }

    /// <summary>
    /// Credit Amount
    /// </summary>
    public double? CreditAmount { get; set; }

    /// <summary>
    /// Display Name
    /// </summary>
    public string? DisplayName { get; set; }

    /// <summary>
    /// Synchronize ID
    /// </summary>
    public int? SyncId { get; set; }

    /// <summary>
    /// Trade Name
    /// </summary>
    public string? Shortname { get; set; }

    /// <summary>
    /// Tài khoản VA
    /// </summary>
    public string? XVaAccount { get; set; }

    /// <summary>
    /// Ngày duyệt TK VA
    /// </summary>
    public DateTime? XVaApprovedDate { get; set; }

    public virtual ICollection<AccountAnalyticAccount> AccountAnalyticAccounts { get; set; } = new List<AccountAnalyticAccount>();

    public virtual ICollection<AccountMove> AccountMoveCommercialPartners { get; set; } = new List<AccountMove>();

    public virtual ICollection<AccountMove> AccountMovePartnerInvoices { get; set; } = new List<AccountMove>();

    public virtual ICollection<AccountMove> AccountMovePartnerShippings { get; set; } = new List<AccountMove>();

    public virtual ICollection<AccountMove> AccountMovePartners { get; set; } = new List<AccountMove>();

    public virtual ICollection<AccountMove> AccountMoveVatPartners { get; set; } = new List<AccountMove>();

    public virtual ICollection<AccountPayment> AccountPaymentPartners { get; set; } = new List<AccountPayment>();

    public virtual ICollection<AccountPayment> AccountPaymentVatPartners { get; set; } = new List<AccountPayment>();

    public virtual ResUser? Buyer { get; set; }

    public virtual ResPartner? CommercialPartner { get; set; }

    public virtual ResUser? CreateU { get; set; }

    public virtual ResDistrict? District { get; set; }

    public virtual ICollection<ResPartner> InverseCommercialPartner { get; set; } = new List<ResPartner>();

    public virtual ICollection<ResPartner> InverseParent { get; set; } = new List<ResPartner>();

    public virtual ResPartner? Parent { get; set; }

    public virtual ICollection<ResUser> ResUsers { get; set; } = new List<ResUser>();

    public virtual ICollection<SaleOrder> SaleOrderPartnerInvoices { get; set; } = new List<SaleOrder>();

    public virtual ICollection<SaleOrder> SaleOrderPartnerShippings { get; set; } = new List<SaleOrder>();

    public virtual ICollection<SaleOrder> SaleOrderPartners { get; set; } = new List<SaleOrder>();

    public virtual ICollection<SaleOrder> SaleOrderVatPartners { get; set; } = new List<SaleOrder>();

    public virtual ResCountryState? State { get; set; }

    public virtual ICollection<StockLot> StockLots { get; set; } = new List<StockLot>();

    public virtual ResUser? User { get; set; }

    public virtual ResWard? Ward { get; set; }

    public virtual ResUser? WriteU { get; set; }
}
