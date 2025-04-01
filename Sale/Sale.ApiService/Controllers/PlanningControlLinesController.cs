using Domain.Core.Errors;
using Domain.Core.Primitives.Result;
using Microsoft.AspNetCore.Mvc;
using Sale.ApiService.Contracts;
using Sale.Application.PlanningControlLines.Commands.Create;
using Sale.Application.PlanningControlLines.Commands.Delete;
using Sale.Contract.PlanningControlLines;

namespace Sale.ApiService.Controllers;
public class PlanningControlLinesController(IMediator mediator) : ApiController(mediator)
{
    /// <summary>
    /// Creates a new planning control line.
    /// </summary>
    /// <param name="planningControlId">The unique identifier of the planning control.</param>
    /// <param name="request">The request containing the necessary data to create a new planning control line.</param>
    /// <returns>An IActionResult indicating the success or failure of the operation.</returns>
    [HttpPost(ApiRoutes.PlanningControlLines.Create)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreatePlanningControlLine(Guid planningControlId, [FromBody] CreatePlanningControlLineRequest request)
        => await Result.Create(request, DomainErrors.General.UnProcessableRequest)
            .Map(request => new CreatePlanningControlLineCommand(planningControlId, request.TimeFrameId, request.IsOriginalBudget, request.IsTarget))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);

    /// <summary>
    /// Deletes a planning control line.
    /// </summary>
    /// <param name="planningControlId">The unique identifier of the planning control.</param>
    /// <param name="planningControlLineId">The unique identifier of the planning control line to be deleted.</param>
    /// <returns>An IActionResult indicating the success or failure of the operation.</returns>
    [HttpDelete(ApiRoutes.PlanningControlLines.Delete)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeletePlanningControlLine(Guid planningControlId, Guid planningControlLineId) =>
        await Result.Create(new { planningControlId, planningControlLineId }, DomainErrors.General.UnProcessableRequest)
        .Map(request => new DeletePlanningControlLineCommand(planningControlId, planningControlLineId))
        .Bind(command => Mediator.Send(command))
        .Match(Ok, BadRequest);
}