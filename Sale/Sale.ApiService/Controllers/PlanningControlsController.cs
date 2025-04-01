using Contract.Common;
using Domain.Core.Errors;
using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using Microsoft.AspNetCore.Mvc;
using Sale.ApiService.Contracts;
using Sale.Application.PlanningControls.Commands.Create;
using Sale.Application.PlanningControls.Commands.Updates.Status;
using Sale.Application.PlanningControls.Commands.Updates.Update;
using Sale.Application.PlanningControls.Queries.PlanningControlById;
using Sale.Application.PlanningControls.Queries.PlanningControls;
using Sale.Contract.PlanningControls;
using Sale.Domain.Enumerations;

namespace Sale.ApiService.Controllers;

public class PlanningControlsController(IMediator mediator) : ApiController(mediator)
{
    /// <summary>
    /// Retrieves a list of available planning control statuses.
    /// </summary>
    /// <returns>A list of <see cref="PlanningControlStatus"/>.</returns>
    [HttpGet(ApiRoutes.PlanningControls.GetStatuses)]
    [ProducesResponseType(typeof(List<PlanningControlStatus>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public IActionResult GetStatuses() => Ok(PlanningControlStatus.List);

    /// <summary>
    /// Retrieves a paginated list of planning controls based on the provided parameters.
    /// </summary>
    /// <param name="pageNumber">The page number to retrieve.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <param name="status">Optional filter for planning control status.</param>
    /// <returns>A paginated list of <see cref="PlanningControlResponse"/>.</returns>
    [HttpGet(ApiRoutes.PlanningControls.Get)]
    [ProducesResponseType(typeof(PagedList<PlanningControlResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPlanningControls(int pageNumber, int pageSize, int? status)
        => await Maybe<PlanningControlsQuery>.From(new PlanningControlsQuery(pageNumber, pageSize, status))
            .Bind(query => Mediator.Send(query))
            .Match(Ok, BadRequest);

    /// <summary>
    /// Retrieves a specific planning control by its unique identifier.
    /// </summary>
    /// <param name="planningControlId">The unique identifier of the planning control.</param>
    /// <returns>A <see cref="PlanningControlResponse"/> if found, otherwise 404 Not Found.</returns>
    [HttpGet(ApiRoutes.PlanningControls.GetById)]
    [ProducesResponseType(typeof(PlanningControlResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPlanningControlById(Guid planningControlId) =>
        await Maybe<PlanningControlByIdQuery>.From(new PlanningControlByIdQuery(planningControlId))
            .Bind(query => Mediator.Send(query))
            .Match(Ok, NotFound);

    /// <summary>
    /// Creates a new planning control with the provided name.
    /// </summary>
    /// <param name="request">The request containing the name of the new planning control.</param>
    /// <returns>200 OK if successful, otherwise 400 Bad Request.</returns>
    [HttpPost(ApiRoutes.PlanningControls.Create)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreatePlanningControl([FromBody] CreatePlanningControlRequest request)
        => await Result.Create(request, DomainErrors.General.UnProcessableRequest)
        .Map(request => new CreatePlanningControlCommand(request.Name))
        .Bind(command => Mediator.Send(command))
        .Match(Ok, BadRequest);

    /// <summary>
    /// Updates an existing planning control with the provided name.
    /// </summary>
    /// <param name="planningControlId">The unique identifier of the planning control to update.</param>
    /// <param name="request">The request containing the new name for the planning control.</param>
    /// <returns>200 OK if successful, otherwise 400 Bad Request.</returns>
    [HttpPut(ApiRoutes.PlanningControls.Update)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdatePlanningControl(Guid planningControlId, [FromBody] CreatePlanningControlRequest request) =>
        await Result.Create(request, DomainErrors.General.UnProcessableRequest)
        .Map(request => new UpdatePlanningControlCommand(planningControlId, request.Name))
        .Bind(command => Mediator.Send(command))
        .Match(Ok, BadRequest);

    /// <summary>
    /// Updates the status of an existing planning control.
    /// </summary>
    /// <param name="planningControlId">The unique identifier of the planning control to update.</param>
    /// <param name="request">The request containing the new status for the planning control.</param>
    /// <returns>200 OK if successful, otherwise 400 Bad Request.</returns>
    [HttpPut(ApiRoutes.PlanningControls.UpdateStatus)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdatePlanningControlStatus(Guid planningControlId, [FromBody] UpdatePlanningControlStatusRequest request) =>
        await Result.Create(request, DomainErrors.General.UnProcessableRequest)
        .Map(request => new UpdatePlanningControlStatusCommand(planningControlId, request.Status))
        .Bind(command => Mediator.Send(command))
        .Match(Ok, BadRequest);
}
