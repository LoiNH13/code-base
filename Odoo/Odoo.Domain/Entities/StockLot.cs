using System;
using System.Collections.Generic;

namespace Odoo.Domain.Entities;

/// <summary>
/// Lot/Serial
/// </summary>
public partial class StockLot
{
    public int Id { get; set; }

    /// <summary>
    /// Product
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Unit of Measure
    /// </summary>
    public int? ProductUomId { get; set; }

    /// <summary>
    /// Company
    /// </summary>
    public int CompanyId { get; set; }

    /// <summary>
    /// Location
    /// </summary>
    public int? LocationId { get; set; }

    /// <summary>
    /// Created by
    /// </summary>
    public int? CreateUid { get; set; }

    /// <summary>
    /// Last Updated by
    /// </summary>
    public int? WriteUid { get; set; }

    /// <summary>
    /// Lot/Serial Number
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Internal Reference
    /// </summary>
    public string? Ref { get; set; }

    /// <summary>
    /// Properties
    /// </summary>
    public string? LotProperties { get; set; }

    /// <summary>
    /// Description
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Created on
    /// </summary>
    public DateTime? CreateDate { get; set; }

    /// <summary>
    /// Last Updated on
    /// </summary>
    public DateTime? WriteDate { get; set; }

    /// <summary>
    /// Expiry has been reminded
    /// </summary>
    public bool? ProductExpiryReminded { get; set; }

    /// <summary>
    /// Expiration Date
    /// </summary>
    public DateTime? ExpirationDate { get; set; }

    /// <summary>
    /// Best before Date
    /// </summary>
    public DateTime? UseDate { get; set; }

    /// <summary>
    /// Removal Date
    /// </summary>
    public DateTime? RemovalDate { get; set; }

    /// <summary>
    /// Alert Date
    /// </summary>
    public DateTime? AlertDate { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    public int? CompanyCurrencyId { get; set; }

    /// <summary>
    /// Cost Unit
    /// </summary>
    public decimal? CostUnit { get; set; }

    /// <summary>
    /// Cost Value
    /// </summary>
    public decimal? Value { get; set; }

    /// <summary>
    /// Customer
    /// </summary>
    public int? LotCustomerId { get; set; }

    /// <summary>
    /// Salesperson
    /// </summary>
    public int? SalespersonId { get; set; }

    /// <summary>
    /// Location
    /// </summary>
    public int? LotLocationId { get; set; }

    /// <summary>
    /// Serial 2
    /// </summary>
    public string? SerialName { get; set; }

    /// <summary>
    /// Type
    /// </summary>
    public string? LotSerialnumberType { get; set; }

    /// <summary>
    /// Shipments
    /// </summary>
    public string? LotShipment { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    public string? LotState { get; set; }

    /// <summary>
    /// Check Stock
    /// </summary>
    public string? CheckStockDate { get; set; }

    /// <summary>
    /// Import Date
    /// </summary>
    public DateOnly? LotImportSnDate { get; set; }

    /// <summary>
    /// Exchange Serial Number Date
    /// </summary>
    public DateOnly? LotExSnDate { get; set; }

    /// <summary>
    /// Consignment Date
    /// </summary>
    public DateOnly? ConsignmentDate { get; set; }

    /// <summary>
    /// Warranty Start Date
    /// </summary>
    public DateOnly? LotWarrantyStartDate { get; set; }

    /// <summary>
    /// Warranty End Date
    /// </summary>
    public DateOnly? LotWarrantyEndDate { get; set; }

    /// <summary>
    /// Filter Datetime
    /// </summary>
    public DateOnly? FilterDatetime { get; set; }

    /// <summary>
    /// Notes
    /// </summary>
    public string? LotNote { get; set; }

    /// <summary>
    /// Documents
    /// </summary>
    public string? LotDocsSource { get; set; }

    /// <summary>
    /// Orders
    /// </summary>
    public string? LotOrderSource { get; set; }

    /// <summary>
    /// Online Activated Date
    /// </summary>
    public DateTime? OnlineActiveDate { get; set; }

    /// <summary>
    /// Manual Activated Date
    /// </summary>
    public DateTime? ManualActiveDate { get; set; }

    /// <summary>
    /// Installation Date
    /// </summary>
    public DateOnly? InstallationDate { get; set; }

    public virtual ResUser? CreateU { get; set; }

    public virtual ResPartner? LotCustomer { get; set; }

    public virtual ProductProduct Product { get; set; } = null!;

    public virtual ResUser? Salesperson { get; set; }

    public virtual ResUser? WriteU { get; set; }
}
