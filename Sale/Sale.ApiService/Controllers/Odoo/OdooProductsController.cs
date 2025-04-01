using Contract.Common;
using Domain.Core.Primitives.Maybe;
using Microsoft.AspNetCore.Mvc;
using Sale.ApiService.Contracts;
using Sale.Application.Odoo.Products.Queries.Products;
using Sale.Contract.Odoo.Products;

namespace Sale.ApiService.Controllers.Odoo;

public class OdooProductsController(IMediator mediator) : ApiController(mediator)
{
    /// <summary>
    /// Retrieves a list of Odoo products based on the provided pagination and search criteria.
    /// </summary>
    /// <param name="pageNumber">The page number for pagination.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <param name="searchText">An optional search string to filter products.</param>
    /// <returns>
    /// An <see cref="IActionResult"/> containing a <see cref="PagedList{OdooProductResponse}"/> if successful,
    /// or an <see cref="ApiErrorResponse"/> if an error occurs.
    /// </returns>
    [HttpGet(ApiRoutes.OdooProducts.Get)]
    [ProducesResponseType(typeof(PagedList<OdooProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetOdooProducts(int pageNumber, int pageSize, string? searchText)
        => await Maybe<OdooProductsQuery>.From(new OdooProductsQuery(pageNumber, pageSize, searchText))
            .Bind(query => Mediator.Send(query))
            .Match(Ok, BadRequest);
}
