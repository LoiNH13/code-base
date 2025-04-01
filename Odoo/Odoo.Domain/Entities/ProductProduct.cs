using System;
using System.Collections.Generic;

namespace Odoo.Domain.Entities;

/// <summary>
/// Product Variant
/// </summary>
public partial class ProductProduct
{
    public int Id { get; set; }

    /// <summary>
    /// Product Template
    /// </summary>
    public int ProductTmplId { get; set; }

    /// <summary>
    /// Created by
    /// </summary>
    public int? CreateUid { get; set; }

    /// <summary>
    /// Last Updated by
    /// </summary>
    public int? WriteUid { get; set; }

    /// <summary>
    /// Internal Reference
    /// </summary>
    public string? DefaultCode { get; set; }

    /// <summary>
    /// Barcode
    /// </summary>
    public string? Barcode { get; set; }

    /// <summary>
    /// Combination Indices
    /// </summary>
    public string? CombinationIndices { get; set; }

    /// <summary>
    /// Volume
    /// </summary>
    public decimal? Volume { get; set; }

    /// <summary>
    /// Weight
    /// </summary>
    public decimal? Weight { get; set; }

    /// <summary>
    /// Active
    /// </summary>
    public bool? Active { get; set; }

    /// <summary>
    /// Can Variant Image 1024 be zoomed
    /// </summary>
    public bool? CanImageVariant1024BeZoomed { get; set; }

    /// <summary>
    /// Write Date
    /// </summary>
    public DateTime? WriteDate { get; set; }

    /// <summary>
    /// Created on
    /// </summary>
    public DateTime? CreateDate { get; set; }

    /// <summary>
    /// Lot Properties
    /// </summary>
    public string? LotPropertiesDefinition { get; set; }

    /// <summary>
    /// Category 1 ID
    /// </summary>
    public int? CategLv1Id { get; set; }

    /// <summary>
    /// Display Name
    /// </summary>
    public string? DisplayName { get; set; }

    /// <summary>
    /// Category 1
    /// </summary>
    public string? CategoryLv1 { get; set; }

    /// <summary>
    /// Category 2
    /// </summary>
    public string? CategoryLv2 { get; set; }

    /// <summary>
    /// Category 3
    /// </summary>
    public string? CategoryLv3 { get; set; }

    /// <summary>
    /// Sales Price
    /// </summary>
    public decimal? LstPrice { get; set; }

    /// <summary>
    /// V15 Product Code
    /// </summary>
    public string? V15ProductCode { get; set; }

    /// <summary>
    /// Need API
    /// </summary>
    public bool? IsNeedApi { get; set; }

    /// <summary>
    /// Is Accessories Included
    /// </summary>
    public bool? IsAccessoriesIncluded { get; set; }

    /// <summary>
    /// Parent Code
    /// </summary>
    public string? ParentCode { get; set; }

    public int? CategLv2Id { get; set; }

    /// <summary>
    /// Image base64
    /// </summary>
    public string? ImageValue { get; set; }

    public virtual ProductCategory? CategLv1 { get; set; }

    public virtual ProductCategory? CategLv2 { get; set; }

    public virtual ResUser? CreateU { get; set; }

    public virtual ICollection<StockLot> StockLots { get; set; } = new List<StockLot>();

    public virtual ResUser? WriteU { get; set; }
}
