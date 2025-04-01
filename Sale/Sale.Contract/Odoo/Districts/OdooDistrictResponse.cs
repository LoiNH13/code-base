using Odoo.Domain.Entities;
using Sale.Contract.Odoo.Wards;

namespace Sale.Contract.Odoo.Districts;

public class OdooDistrictResponse
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
    /// Country State
    /// </summary>
    public int StateId { get; set; }

    /// <summary>
    /// Country
    /// </summary>
    public int? CountryId { get; set; }

    public List<OdooWardResponse> OdooWards { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="OdooDistrictResponse"/> class.
    /// </summary>
    /// <param name="district">The ResDistrict object to convert to OdooDistrictResponse.</param>
    public OdooDistrictResponse(ResDistrict district)
    {
        Id = district.Id;
        Name = district.Name;
        Code = district.Code;
        StateId = district.StateId;
        CountryId = district.CountryId;

        OdooWards = district.ResWards.Select(x => new OdooWardResponse(x)).ToList();
    }
}