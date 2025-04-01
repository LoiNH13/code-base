using Contract.Common;
using Domain.Core.Primitives.Maybe;
using Microsoft.AspNetCore.Mvc;
using Sale.ApiService.Contracts;
using Sale.Application.Odoo.Customers.Queries.Customers;
using Sale.Contract.Odoo.Customers;

namespace Sale.ApiService.Controllers.Odoo;

/// <summary>
/// This controller handles HTTP requests related to Odoo customers.
/// </summary>
public class OdooCustomersController : ApiController
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OdooCustomersController"/> class.
    /// </summary>
    /// <param name="mediator">An instance of the Mediator pattern for handling application requests.</param>
    public OdooCustomersController(IMediator mediator) : base(mediator)
    {
    }

    /// <summary>
    /// Retrieves a list of Odoo customers based on the provided pagination and search criteria.
    /// </summary>
    /// <param name="PageNumber">The page number for pagination.</param>
    /// <param name="PageSize">The number of items per page.</param>
    /// <param name="searchText">An optional search string to filter customers.</param>
    /// <returns>
    /// A <see cref="Task{IActionResult}"/> representing the asynchronous operation.
    /// The result is an HTTP 200 OK response containing a paginated list of <see cref="OdooCustomerResponse"/> if successful.
    /// If the request is invalid, the result is an HTTP 400 Bad Request response containing an <see cref="ApiErrorResponse"/>.
    /// </returns>
    [HttpGet(ApiRoutes.OdooCustomers.Get)]
    [ProducesResponseType(typeof(PagedList<OdooCustomerResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetOdooCustomers(int PageNumber, int PageSize, string? searchText) =>
        await Maybe<OdooCustomersQuery>.From(new OdooCustomersQuery(PageNumber, PageSize, searchText))
        .Bind(query => Mediator.Send(query))
        .Match(Ok, BadRequest);
}
