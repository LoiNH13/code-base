using System;
using System.Collections.Generic;

namespace Odoo.Domain.Entities;

/// <summary>
/// Product Category
/// </summary>
public partial class ProductCategory
{
    public int Id { get; set; }

    /// <summary>
    /// Parent Category
    /// </summary>
    public int? ParentId { get; set; }

    /// <summary>
    /// Created by
    /// </summary>
    public int? CreateUid { get; set; }

    /// <summary>
    /// Last Updated by
    /// </summary>
    public int? WriteUid { get; set; }

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Complete Name
    /// </summary>
    public string? CompleteName { get; set; }

    /// <summary>
    /// Parent Path
    /// </summary>
    public string? ParentPath { get; set; }

    /// <summary>
    /// Product Properties
    /// </summary>
    public string? ProductPropertiesDefinition { get; set; }

    /// <summary>
    /// Created on
    /// </summary>
    public DateTime? CreateDate { get; set; }

    /// <summary>
    /// Last Updated on
    /// </summary>
    public DateTime? WriteDate { get; set; }

    /// <summary>
    /// Force Removal Strategy
    /// </summary>
    public int? RemovalStrategyId { get; set; }

    /// <summary>
    /// Reserve Packagings
    /// </summary>
    public string? PackagingReserveMethod { get; set; }

    /// <summary>
    /// Level
    /// </summary>
    public int? Level { get; set; }

    /// <summary>
    /// Code
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// Active
    /// </summary>
    public bool? Active { get; set; }

    /// <summary>
    /// Sales Returns Account
    /// </summary>
    public int? PropertySalesReturnsAccountCategId { get; set; }

    /// <summary>
    /// Expense Analysis
    /// </summary>
    public bool? IsExpenseAnalysis { get; set; }

    /// <summary>
    /// Consignment Stock Account
    /// </summary>
    public int? ConsignmentStockAccountId { get; set; }

    /// <summary>
    /// Synchronize ID
    /// </summary>
    public int? SyncId { get; set; }

    public virtual ResUser? CreateU { get; set; }

    public virtual ICollection<ProductCategory> InverseParent { get; set; } = new List<ProductCategory>();

    public virtual ProductCategory? Parent { get; set; }

    public virtual ICollection<ProductProduct> ProductProductCategLv1s { get; set; } = new List<ProductProduct>();

    public virtual ICollection<ProductProduct> ProductProductCategLv2s { get; set; } = new List<ProductProduct>();

    public virtual ResUser? WriteU { get; set; }
}
