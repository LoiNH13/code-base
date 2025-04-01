using Domain.Core.Errors;
using Domain.Core.Primitives.Result;
using Microsoft.AspNetCore.Mvc;
using Sale.ApiService.Contracts;
using Sale.Application.ProductPrices.Commands.Create;
using Sale.Application.ProductPrices.Commands.Delete;
using Sale.Application.ProductPrices.Commands.Update;
using Sale.Contract.TimeFrameProductPrices;

namespace Sale.ApiService.Controllers;

public class TimeFrameProductPricesController(IMediator mediator) : ApiController(mediator)
{
    /// <summary>
    /// Creates a new product price for a specific time frame.
    /// </summary>
    /// <param name="request">The request containing the product ID, time frame ID, and price.</param>
    /// <returns>An IActionResult indicating success or failure.</returns>
    [HttpPost(ApiRoutes.TimeFrameProductPrices.Create)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTimeFrameProductPrice([FromBody] CreateTimeFrameProductPriceRequest request) =>
        await Result.Create(request, DomainErrors.General.UnProcessableRequest)
        .Map(request => new CreateProductPriceCommand(request.ProductId, request.TimeFrameId, request.Price))
        .Bind(command => Mediator.Send(command))
        .Match(Ok, BadRequest);

    /// <summary>
    /// Updates the price of a product for a specific time frame.
    /// </summary>
    /// <param name="productId">The ID of the product.</param>
    /// <param name="timeFrameProductId">The ID of the product price for the specific time frame.</param>
    /// <param name="request">The request containing the new price.</param>
    /// <returns>An IActionResult indicating success or failure.</returns>
    [HttpPut(ApiRoutes.TimeFrameProductPrices.Update)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateTimeFrameProductPrice(Guid productId, Guid timeFrameProductId, [FromBody] UpdateTimeFrameProductPriceRequest request) =>
        await Result.Create(request, DomainErrors.General.UnProcessableRequest)
        .Map(request => new UpdateProductPriceCommand(productId, timeFrameProductId, request.Price))
        .Bind(command => Mediator.Send(command))
        .Match(Ok, BadRequest);

    /// <summary>
    /// Deletes a product price for a specific time frame.
    /// </summary>
    /// <param name="productId">The ID of the product.</param>
    /// <param name="timeFrameProductId">The ID of the product price for the specific time frame.</param>
    /// <returns>An IActionResult indicating success or failure.</returns>
    [HttpDelete(ApiRoutes.TimeFrameProductPrices.Delete)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteTimeFrameProductPrice(Guid productId, Guid timeFrameProductId) =>
        await Result.Create(new { productId, timeFrameProductId }, DomainErrors.General.UnProcessableRequest)
        .Map(request => new DeleteProductPriceCommand(productId, timeFrameProductId))
        .Bind(command => Mediator.Send(command))
        .Match(Ok, BadRequest);
}
