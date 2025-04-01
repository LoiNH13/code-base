using Domain.Core.Errors;
using Domain.Core.Primitives.Result;
using Microsoft.AspNetCore.Mvc;
using Sale.ApiService.Contracts;
using Sale.Application.MonthlyReportItems.Commands.Create;
using Sale.Application.MonthlyReportItems.Commands.Delete;
using Sale.Application.MonthlyReportItems.Commands.Updates.ListUpdate;
using Sale.Application.MonthlyReportItems.Commands.Updates.Update;
using Sale.Contract.MonthlyReportItems;

namespace Sale.ApiService.Controllers;

public class MonthlyReportItemsController(IMediator mediator) : ApiController(mediator)
{
    /// <summary>
    /// Creates a new monthly report item.
    /// </summary>
    /// <param name="monthlyReportId">The unique identifier of the monthly report.</param>
    /// <param name="request">The request containing the details of the new monthly report item.</param>
    /// <returns>An IActionResult indicating the success or failure of the operation.</returns>
    [HttpPut(ApiRoutes.MonthlyReportItems.Create)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateMonthlyReportItem(Guid monthlyReportId,
        [FromBody] CreateMonthlyReportItemRequest request)
        => await Result.Create(request, DomainErrors.General.UnProcessableRequest)
            .Map(rq => new CreateMonthlyReportItemCommand(monthlyReportId,
                rq.CategoryId,
                rq.Group,
                rq.Quantity,
                rq.Revenue,
                rq.Note))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);

    /// <summary>
    /// Updates an existing monthly report item.
    /// </summary>
    /// <param name="monthlyReportId">The unique identifier of the monthly report.</param>
    /// <param name="monthlyReportItemId">The unique identifier of the monthly report item.</param>
    /// <param name="request">The request containing the updated details of the monthly report item.</param>
    /// <returns>An IActionResult indicating the success or failure of the operation.</returns>
    [HttpPut(ApiRoutes.MonthlyReportItems.Update)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateMonthlyReportItem(Guid monthlyReportId, Guid monthlyReportItemId,
        [FromBody] UpdateMonthlyReportItemRequest request)
        => await Result.Create(request, DomainErrors.General.UnProcessableRequest)
            .Map(request => new UpdateMonthlyReportItemCommand(monthlyReportId,
                monthlyReportItemId,
                request.Quantity,
                request.Revenue,
                request.Note))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);

    /// <summary>
    /// Updates multiple existing monthly report items.
    /// </summary>
    /// <param name="monthlyReportId">The unique identifier of the monthly report.</param>
    /// <param name="request">The list of requests containing the updated details of the monthly report items.</param>
    /// <returns>An IActionResult indicating the success or failure of the operation.</returns>
    [HttpPut(ApiRoutes.MonthlyReportItems.Updates)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdatesMonthlyReportItem(Guid monthlyReportId,
        [FromBody] List<UpdateMonthlyReportItem2Request> request)
        => await Result.Create(request, DomainErrors.General.UnProcessableRequest)
            .Map(request => new ListUpdateMonthlyReportItemCommand(monthlyReportId, request))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);

    /// <summary>
    /// Deletes an existing monthly report item.
    /// </summary>
    /// <param name="monthlyReportId">The unique identifier of the monthly report.</param>
    /// <param name="monthlyReportItemId">The unique identifier of the monthly report item.</param>
    /// <returns>An IActionResult indicating the success or failure of the operation.</returns>
    [HttpDelete(ApiRoutes.MonthlyReportItems.Delete)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteMonthlyReportItem(Guid monthlyReportId, Guid monthlyReportItemId)
    {
        return await Result.Create(new { MonthlyReportId = monthlyReportId, MonthlyReportItemId = monthlyReportItemId },
                DomainErrors.General.UnProcessableRequest)
            .Map(request => new DeleteMonthlyReportItemCommand(request.MonthlyReportId, request.MonthlyReportItemId))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);
    }
}