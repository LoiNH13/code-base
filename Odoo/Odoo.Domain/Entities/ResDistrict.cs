using System;
using System.Collections.Generic;

namespace Odoo.Domain.Entities;

/// <summary>
/// District
/// </summary>
public partial class ResDistrict
{
    public int Id { get; set; }

    /// <summary>
    /// Country State
    /// </summary>
    public int StateId { get; set; }

    /// <summary>
    /// Country
    /// </summary>
    public int? CountryId { get; set; }

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
    /// Code
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
    /// Synchronize ID
    /// </summary>
    public int? SyncId { get; set; }

    public virtual ResUser? CreateU { get; set; }

    public virtual ICollection<ResPartner> ResPartners { get; set; } = new List<ResPartner>();

    public virtual ICollection<ResWard> ResWards { get; set; } = new List<ResWard>();

    public virtual ResCountryState State { get; set; } = null!;

    public virtual ResUser? WriteU { get; set; }
}
