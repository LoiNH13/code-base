using Odoo.Domain.Entities;
using Sale.Contract.Odoo.Districts;

namespace Sale.Contract.Odoo.States;

public class OdooStateResponse
{
    public int Id { get; set; }

    /// <summary>
    /// Country
    /// </summary>
    public int CountryId { get; set; }

    /// <summary>
    /// State Name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// State Code
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// List of districts belonging to this state.
    /// </summary>
    public List<OdooDistrictResponse>? OdooDistricts { get; set; }

    /// <summary>
    /// Constructor to initialize an OdooStateResponse object from a ResCountryState object.
    /// </summary>
    /// <param name="state">The ResCountryState object to initialize from.</param>
    public OdooStateResponse(ResCountryState state)
    {
        Id = state.Id;
        CountryId = state.CountryId;
        Name = state.Name;
        Code = state.Code;

        OdooDistricts = state.ResDistricts.Select(x => new OdooDistrictResponse(x)).ToList();
    }
}
