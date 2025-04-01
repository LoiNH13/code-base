using System;
using System.Collections.Generic;

namespace Odoo.Domain.Entities;

/// <summary>
/// Country state
/// </summary>
public partial class ResCountryState
{
    public int Id { get; set; }

    /// <summary>
    /// Country
    /// </summary>
    public int CountryId { get; set; }

    /// <summary>
    /// Created by
    /// </summary>
    public int? CreateUid { get; set; }

    /// <summary>
    /// Last Updated by
    /// </summary>
    public int? WriteUid { get; set; }

    /// <summary>
    /// State Name
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// State Code
    /// </summary>
    public string Code { get; set; } = null!;

    /// <summary>
    /// Created on
    /// </summary>
    public DateTime? CreateDate { get; set; }

    /// <summary>
    /// Last Updated on
    /// </summary>
    public DateTime? WriteDate { get; set; }

    /// <summary>
    /// Area
    /// </summary>
    public int? AreaId { get; set; }

    /// <summary>
    /// Synchronize ID
    /// </summary>
    public int? SyncId { get; set; }

    public virtual ResUser? CreateU { get; set; }

    public virtual ICollection<ResDistrict> ResDistricts { get; set; } = new List<ResDistrict>();

    public virtual ICollection<ResPartner> ResPartners { get; set; } = new List<ResPartner>();

    public virtual ICollection<ResWard> ResWards { get; set; } = new List<ResWard>();

    public virtual ResUser? WriteU { get; set; }
}
