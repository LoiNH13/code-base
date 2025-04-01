using Contract.Common;
using Domain.Core.Primitives.Maybe;
using Microsoft.AspNetCore.Mvc;
using Sale.ApiService.Contracts;
using Sale.Application.Odoo.Districts.Queries.DistrictById;
using Sale.Application.Odoo.Districts.Queries.Districts;
using Sale.Application.Odoo.States.Queries.StateById;
using Sale.Application.Odoo.States.Queries.States;
using Sale.Application.Odoo.Wards.Queries.WardById;
using Sale.Application.Odoo.Wards.Queries.Wards;
using Sale.Contract.Odoo.Districts;
using Sale.Contract.Odoo.States;
using Sale.Contract.Odoo.Wards;

namespace Sale.ApiService.Controllers.Odoo;

public class OdooCommonsController(IMediator mediator) : ApiController(mediator)
{
    /// <summary>
    /// Retrieves a paginated list of Odoo states based on the provided parameters.
    /// </summary>
    /// <param name="pageNumber">The page number to retrieve.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <param name="searchText">An optional search text to filter the states.</param>
    /// <returns>A paginated list of Odoo states or an error response if the request is invalid.</returns>
    [HttpGet(ApiRoutes.OdooCommons.GetStates)]
    [ProducesResponseType(typeof(PagedList<OdooStateResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetStates(int pageNumber, int pageSize, string? searchText) =>
        await Maybe<OdooStatesQuery>.From(new OdooStatesQuery(pageNumber, pageSize, searchText))
        .Bind(query => Mediator.Send(query))
        .Match(Ok, BadRequest);

    /// <summary>
    /// Retrieves a specific Odoo state by its ID.
    /// </summary>
    /// <param name="stateId">The ID of the Odoo state to retrieve.</param>
    /// <returns>The requested Odoo state or a 404 Not Found status if the state does not exist.</returns>
    [HttpGet(ApiRoutes.OdooCommons.GetStateById)]
    [ProducesResponseType(typeof(OdooStateResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetStateById(int stateId) =>
        await Maybe<OdooStateByIdQuery>.From(new OdooStateByIdQuery(stateId))
        .Bind(query => Mediator.Send(query))
        .Match(Ok, NotFound);

    /// <summary>
    /// Retrieves a paginated list of Odoo districts based on the provided parameters.
    /// </summary>
    /// <param name="pageNumber">The page number to retrieve.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <param name="searchText">An optional search text to filter the districts.</param>
    /// <param name="stateId">An optional state ID to filter the districts.</param>
    /// <returns>A paginated list of Odoo districts or an error response if the request is invalid.</returns>
    [HttpGet(ApiRoutes.OdooCommons.GetDistricts)]
    [ProducesResponseType(typeof(List<OdooDistrictResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetDistricts(int pageNumber, int pageSize, string? searchText, int? stateId) =>
        await Maybe<OdooDistrictsQuery>.From(new OdooDistrictsQuery(pageNumber, pageSize, stateId, searchText))
        .Bind(query => Mediator.Send(query))
        .Match(Ok, BadRequest);

    /// <summary>
    /// Retrieves a specific Odoo district by its ID.
    /// </summary>
    /// <param name="districtId">The ID of the Odoo district to retrieve.</param>
    /// <returns>The requested Odoo district or a 404 Not Found status if the district does not exist.</returns>
    [HttpGet(ApiRoutes.OdooCommons.GetDistrictById)]
    [ProducesResponseType(typeof(OdooDistrictResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDistrictById(int districtId) =>
        await Maybe<OdooDistrictByIdQuery>.From(new OdooDistrictByIdQuery(districtId))
        .Bind(query => Mediator.Send(query))
        .Match(Ok, NotFound);

    /// <summary>
    /// Retrieves a paginated list of Odoo wards based on the provided parameters.
    /// </summary>
    /// <param name="pageNumber">The page number to retrieve.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <param name="searchText">An optional search text to filter the wards.</param>
    /// <param name="districtId">An optional district ID to filter the wards.</param>
    /// <returns>A paginated list of Odoo wards or an error response if the request is invalid.</returns>
    [HttpGet(ApiRoutes.OdooCommons.GetWards)]
    [ProducesResponseType(typeof(List<OdooWardResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetWards(int pageNumber, int pageSize, string? searchText, int? districtId) =>
        await Maybe<OdooWardsQuery>.From(new OdooWardsQuery(pageNumber, pageSize, districtId, searchText))
        .Bind(query => Mediator.Send(query))
        .Match(Ok, BadRequest);

    /// <summary>
    /// Retrieves a specific Odoo ward by its ID.
    /// </summary>
    /// <param name="wardId">The ID of the Odoo ward to retrieve.</param>
    /// <returns>The requested Odoo ward or a 404 Not Found status if the ward does not exist.</returns>
    [HttpGet(ApiRoutes.OdooCommons.GetWardById)]
    [ProducesResponseType(typeof(OdooWardResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetWardById(int wardId) =>
        await Maybe<OdooWardByIdQuery>.From(new OdooWardByIdQuery(wardId))
        .Bind(query => Mediator.Send(query))
        .Match(Ok, NotFound);
}
