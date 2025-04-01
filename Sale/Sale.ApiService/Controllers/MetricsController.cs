using Contract.Common;
using Domain.Core.Errors;
using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using Microsoft.AspNetCore.Mvc;
using Sale.ApiService.Contracts;
using Sale.Application.Metrics.Commands.AddsOrUpdates;
using Sale.Application.Metrics.Queries.Metrics.Forecast;
using Sale.Application.Metrics.Queries.SummaryByCustomer;
using Sale.Contract.Metrics;

namespace Sale.ApiService.Controllers;

public class MetricsController(IMediator mediator) : ApiController(mediator)
{
    /// <summary>
    /// Retrieves a summary of metrics for a specific customer.
    /// </summary>
    /// <param name="customerId">The unique identifier of the customer.</param>
    /// <param name="request">The request object containing the required parameters for the query.</param>
    /// <returns>A list of summary metrics for the customer or an error response if the request is invalid.</returns>
    [HttpPost(ApiRoutes.Metrics.Summary)]
    [ProducesResponseType(typeof(List<SummaryByCustomerResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetSummaryMetrics(Guid customerId, [FromBody] SummaryByCustomerRequest request) => await Maybe<GetSummaryByCustomerQuery>
            .From(new GetSummaryByCustomerQuery(customerId, request.FromMY, request.ToMY, request.MetricType))
            .Bind(query => Mediator.Send(query))
            .Match(Ok, BadRequest);

    /// <summary>
    /// Retrieves forecast metrics for a specific customer.
    /// </summary>
    /// <param name="customerId">The unique identifier of the customer.</param>
    /// <param name="request">The request object containing the required parameters for the query.</param>
    /// <returns>A paged list of forecast metrics for the customer or an error response if the request is invalid.</returns>
    [HttpPost(ApiRoutes.Metrics.GetForeCastMetrics)]
    [ProducesResponseType(typeof(PagedList<GetMetricsResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetForeCastMetrics(Guid customerId, [FromBody] GetMetricsRequest request)
        => await Maybe<GetForecastMetricsQuery>.From(new GetForecastMetricsQuery(request.PageNumber,
                request.PageSize,
                request.FromMY,
                request.ToMY,
                customerId,
                request.CategoryId,
                request.ProductId))
            .Bind(query => Mediator.Send(query))
            .Match(Ok, BadRequest);

    /// <summary>
    /// Adds or updates product metrics for a specific customer.
    /// </summary>
    /// <param name="customerId">The unique identifier of the customer.</param>
    /// <param name="request">The list of product metrics to be added or updated.</param>
    /// <returns>An HTTP 200 OK response if the operation is successful or an error response if the request is invalid.</returns>
    [HttpPut(ApiRoutes.Metrics.AddsOrUpdates)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddOrUpdateMetrics(Guid customerId,
        [FromBody] List<AddOrUpdateProductMetricsDto> request)
        => await Result.Create(request, DomainErrors.General.UnProcessableRequest)
            .Map(rq => new AddOrUpdateMetricsCommand(customerId, rq))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);
}