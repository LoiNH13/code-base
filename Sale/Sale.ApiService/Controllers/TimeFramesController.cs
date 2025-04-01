using Contract.Common;
using Domain.Core.Errors;
using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using Microsoft.AspNetCore.Mvc;
using Sale.ApiService.Contracts;
using Sale.Application.TimeFrames.Commands.Creates.Create;
using Sale.Application.TimeFrames.Queries.TimeFrameById;
using Sale.Application.TimeFrames.Queries.TimeFrames;
using Sale.Application.TimeFrames.Queries.TimeFramesByRange;
using Sale.Contract.TimeFrames;

namespace Sale.ApiService.Controllers;

public class TimeFramesController(IMediator mediator) : ApiController(mediator)
{
    /// <summary>
    /// Retrieves a paginated list of time frames based on the provided parameters.
    /// </summary>
    /// <param name="pageNumber">The page number to retrieve.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <param name="year">Optional parameter to filter time frames by year.</param>
    /// <returns>A paginated list of time frames or an error response if the request is invalid.</returns>
    [HttpGet(ApiRoutes.TimeFrames.Get)]
    [ProducesResponseType(typeof(PagedList<TimeFrameResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetTimeFrames(int pageNumber, int pageSize, int? year) =>
        await Maybe<TimeFramesQuery>.From(new TimeFramesQuery(pageNumber, pageSize, year))
        .Bind(query => Mediator.Send(query))
        .Match(Ok, BadRequest);

    /// <summary>
    /// Retrieves a list of time frames within a specified date range.
    /// </summary>
    /// <param name="fromYear">The starting year of the range.</param>
    /// <param name="fromMonth">The starting month of the range.</param>
    /// <param name="toYear">The ending year of the range.</param>
    /// <param name="toMonth">The ending month of the range.</param>
    /// <returns>A list of time frames within the specified range or an error response if the request is invalid.</returns>
    [HttpGet(ApiRoutes.TimeFrames.GetRange)]
    [ProducesResponseType(typeof(List<TimeFrameResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetTimeFrameRange(int fromYear, int fromMonth, int toYear, int toMonth) =>
        await Maybe<GetTimeFramesByRangeQuery>.From(new GetTimeFramesByRangeQuery(fromYear, fromMonth, toYear, toMonth))
        .Bind(query => Mediator.Send(query))
        .Match(Ok, BadRequest);

    /// <summary>
    /// Retrieves a specific time frame by its unique identifier.
    /// </summary>
    /// <param name="timeFrameId">The unique identifier of the time frame.</param>
    /// <returns>The requested time frame or a 404 Not Found status if the time frame does not exist.</returns>
    [HttpGet(ApiRoutes.TimeFrames.GetById)]
    [ProducesResponseType(typeof(TimeFrameResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTimeFrameById(Guid timeFrameId) =>
        await Maybe<TimeFrameByIdQuery>.From(new TimeFrameByIdQuery(timeFrameId))
        .Bind(query => Mediator.Send(query))
        .Match(Ok, BadRequest);

    /// <summary>
    /// Creates a new time frame based on the provided request data.
    /// </summary>
    /// <param name="request">The request data containing the year and month for the new time frame.</param>
    /// <returns>An OK status if the time frame is created successfully or a bad request status if the request is invalid.</returns>
    [HttpPost(ApiRoutes.TimeFrames.Create)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTimeFrame([FromBody] CreateTimeFrameRequest request) =>
        await Result.Create(request, DomainErrors.General.UnProcessableRequest)
            .Map(request => new CreateTimeFrameCommand(request.Year, request.Month))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);
}
