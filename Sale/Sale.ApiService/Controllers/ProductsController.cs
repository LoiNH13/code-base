using Contract.Common;
using Domain.Core.Errors;
using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using Microsoft.AspNetCore.Mvc;
using Sale.ApiService.Contracts;
using Sale.Application.Products.Commands.Create;
using Sale.Application.Products.Commands.Updates.Status;
using Sale.Application.Products.Commands.Updates.Update;
using Sale.Application.Products.Queries.ProductById;
using Sale.Application.Products.Queries.Products;
using Sale.Contract.Products;

namespace Sale.ApiService.Controllers;

public class ProductsController(IMediator mediator) : ApiController(mediator)
{
    /// <summary>
    /// Retrieves a paginated list of products based on the provided parameters.
    /// </summary>
    /// <param name="pageNumber">The page number to retrieve.</param>
    /// <param name="pageSize">The number of products per page.</param>
    /// <param name="searchText">An optional search text to filter products by name.</param>
    /// <param name="categoryId">An optional category ID to filter products by category.</param>
    /// <returns>A paginated list of products or an error response if the request is invalid.</returns>
    [HttpGet(ApiRoutes.Products.Get)]
    [ProducesResponseType(typeof(PagedList<ProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetProducts(int pageNumber, int pageSize, string? searchText, Guid? categoryId)
        => await Maybe<ProductsQuery>.From(new ProductsQuery(pageNumber, pageSize, searchText, categoryId))
            .Bind(query => Mediator.Send(query))
            .Match(Ok, BadRequest);

    /// <summary>
    /// Retrieves a single product by its ID.
    /// </summary>
    /// <param name="productId">The ID of the product to retrieve.</param>
    /// <returns>The requested product or a 404 Not Found response if the product does not exist.</returns>
    [HttpGet(ApiRoutes.Products.GetById)]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProductById(Guid productId)
        => await Maybe<ProductByIdQuery>.From(new ProductByIdQuery(productId))
            .Bind(query => Mediator.Send(query))
            .Match(Ok, BadRequest);

    /// <summary>
    /// Creates a new product using the provided request data.
    /// </summary>
    /// <param name="request">The request data containing the product details.</param>
    /// <returns>An OK response if the product is created successfully, or a bad request response if the request is invalid.</returns>
    [HttpPost(ApiRoutes.Products.Create)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request) =>
        await Result.Create(request, DomainErrors.General.UnProcessableRequest)
        .Map(request => new CreateProductCommand(request.CategoryId, request.Name, request.OdooRef, request.Weight, request.Price))
        .Bind(command => Mediator.Send(command))
        .Match(Ok, BadRequest);

    /// <summary>
    /// Updates an existing product using the provided request data.
    /// </summary>
    /// <param name="productId">The ID of the product to update.</param>
    /// <param name="request">The request data containing the updated product details.</param>
    /// <returns>An OK response if the product is updated successfully, or a bad request response if the request is invalid.</returns>
    [HttpPut(ApiRoutes.Products.Update)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateProduct(Guid productId, [FromBody] CreateProductRequest request) =>
        await Result.Create(request, DomainErrors.General.UnProcessableRequest)
        .Map(request => new UpdateProductCommand(productId, request.Name, request.OdooRef, request.CategoryId, request.Weight, request.Price))
        .Bind(command => Mediator.Send(command))
        .Match(Ok, BadRequest);

    /// <summary>
    /// Updates the status of an existing product.
    /// </summary>
    /// <param name="productId">The ID of the product to update.</param>
    /// <returns>An OK response if the product status is updated successfully, or a bad request response if the request is invalid.</returns>
    [HttpPut(ApiRoutes.Products.UpdateStatus)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateProductStatus(Guid productId) =>
        await Result.Create(new { productId }, DomainErrors.General.UnProcessableRequest)
        .Map(request => new UpdateProductStatusCommand(productId))
        .Bind(command => Mediator.Send(command))
        .Match(Ok, BadRequest);
}