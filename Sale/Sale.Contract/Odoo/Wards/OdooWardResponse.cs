using Odoo.Domain.Entities;

namespace Sale.Contract.Odoo.Wards;

public class OdooWardResponse
{
    public int Id { get; set; }

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Code
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// District
    /// </summary>
    public int DistrictId { get; set; }

    /// <summary>
    /// Country State
    /// </summary>
    public int? StateId { get; set; }

    /// <summary>
    /// Country
    /// </summary>
    public int? CountryId { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="OdooWardResponse"/> class.
    /// Constructs an OdooWardResponse object from a ResWard object.
    /// </summary>
    /// <param name="ward">The ResWard object to construct the OdooWardResponse from.</param>
    public OdooWardResponse(ResWard ward)
    {
        Id = ward.Id;
        Name = ward.Name;
        Code = ward.Code;
        DistrictId = ward.DistrictId;
        StateId = ward.StateId;
        CountryId = ward.CountryId;
    }
}