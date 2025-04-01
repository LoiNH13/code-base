using Contract.Common;
using Domain.Core.Errors;
using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using Microsoft.AspNetCore.Mvc;
using Sale.ApiService.Contracts;
using Sale.Application.Categories.Commands.Create;
using Sale.Application.Categories.Commands.Update;
using Sale.Application.Categories.Queries.Categories;
using Sale.Contract.Categories;

namespace Sale.ApiService.Controllers;

public class CategoriesController(IMediator mediator) : ApiController(mediator)
{
    /// <summary>
    /// Retrieves a paged list of categories based on specified criteria.
    /// </summary>
    /// <param name="pageNumber">The number of the page to retrieve.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <param name="searchText">Optional. The text to search for in category names.</param>
    /// <param name="showInApp">Optional. Filter for categories to be shown in the app.</param>
    /// <returns>
    /// A Task that represents the asynchronous operation, containing an IActionResult.
    /// The result is either:
    /// - 200 OK with a PagedList of CategoryResponse objects if successful.
    /// - 400 Bad Request with an ApiErrorResponse if the request is invalid.
    /// </returns>
    [HttpGet(ApiRoutes.Categories.Get)]
    [ProducesResponseType(typeof(PagedList<CategoryResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCategories([FromQuery] int pageNumber, int pageSize, string? searchText,
        EShowInApp? showInApp)
    {
        return await Maybe<CategoriesQuery>.From(new CategoriesQuery(pageNumber, pageSize, searchText, showInApp))
            .Bind(query => Mediator.Send(query))
            .Match(Ok, BadRequest);
    }

    /// <summary>
    /// Creates a new category based on the provided request data.
    /// </summary>
    /// <param name="request">The CreateCategoryRequest object containing the details of the category to be created.</param>
    /// <returns>
    /// A Task that represents the asynchronous operation, containing an IActionResult.
    /// The result is either:
    /// - 200 OK if the category is successfully created.
    /// - 400 Bad Request with an ApiErrorResponse if the request is invalid or the creation fails.
    /// </returns>
    [HttpPost(ApiRoutes.Categories.Create)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest request) =>
        await Result.Create(request, DomainErrors.General.UnProcessableRequest)
            .Map(rq => new CreateCategoryCommand(rq.Name, rq.OdooRef, rq.Weight, rq.IsShowSalePlan,
                rq.IsShowMonthlyReport))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);

    /// <summary>
    /// Updates an existing category with the provided details.
    /// </summary>
    /// <param name="categoryId">The unique identifier of the category to be updated.</param>
    /// <param name="request">The CreateCategoryRequest object containing the updated details of the category.</param>
    /// <returns>
    /// A Task that represents the asynchronous operation, containing an IActionResult.
    /// The result is either:
    /// - 200 OK if the category is successfully updated.
    /// - 400 Bad Request with an ApiErrorResponse if the request is invalid or the update fails.
    /// </returns>
    [HttpPut(ApiRoutes.Categories.Update)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCategory(Guid categoryId, [FromBody] CreateCategoryRequest request) =>
        await Result.Create(request, DomainErrors.General.UnProcessableRequest)
            .Map(rq => new UpdateCategoryCommand(categoryId, rq.Name, rq.OdooRef, rq.Weight, rq.IsShowSalePlan,
                rq.IsShowMonthlyReport))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);
}