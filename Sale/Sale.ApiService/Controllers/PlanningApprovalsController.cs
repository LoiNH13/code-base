using Contract.Common;
using Domain.Core.Errors;
using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using Microsoft.AspNetCore.Mvc;
using Sale.ApiService.Contracts;
using Sale.Application.PlanningApprovals.Commands.Updates.Status.Approved;
using Sale.Application.PlanningApprovals.Commands.Updates.Status.EditRequest;
using Sale.Application.PlanningApprovals.Commands.Updates.Status.Unlock;
using Sale.Application.PlanningApprovals.Commands.Updates.Status.WaitingForApprove;
using Sale.Application.PlanningApprovals.Queries.PlanningApprovals;
using Sale.Application.PlanningApprovals.Queries.StatisticsBySales;
using Sale.Contract.PlanningApprovals;
using Sale.Domain.Enumerations;

namespace Sale.ApiService.Controllers;

public class PlanningApprovalsController(IMediator mediator) : ApiController(mediator)
{
    /// <summary>
    /// Retrieves a list of available planning approval statuses.
    /// </summary>
    /// <returns>A list of <see cref="PlanningApprovalStatus"/>.</returns>
    [HttpGet(ApiRoutes.PlanningApprovals.GetStatuses)]
    [ProducesResponseType(typeof(List<PlanningApprovalStatus>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public IActionResult GetStatuses() => Ok(PlanningApprovalStatus.List);

    /// <summary>
    /// Retrieves a paginated list of planning approvals based on the provided filters.
    /// </summary>
    /// <param name="pageNumber">The page number to retrieve.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <param name="customerId">Optional filter for customer ID.</param>
    /// <param name="planningControlId">Optional filter for planning control ID.</param>
    /// <param name="planningControlStatus">Optional filter for planning control status.</param>
    /// <param name="appproveStatus">Optional filter for approval status.</param>
    /// <returns>A paginated list of <see cref="PlanningApprovalResponse"/>.</returns>
    [HttpGet(ApiRoutes.PlanningApprovals.Get)]
    [ProducesResponseType(typeof(PagedList<PlanningApprovalResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPlanningApprovals(int pageNumber, int pageSize, Guid? customerId,
        Guid? planningControlId, int? planningControlStatus, int? appproveStatus)
    {
        return await Maybe<PlanningApprovalsQuery>.From(new PlanningApprovalsQuery(pageNumber, pageSize, customerId,
                planningControlStatus, appproveStatus, planningControlId))
            .Bind(query => Mediator.Send(query))
            .Match(Ok, BadRequest);
    }

    /// <summary>
    /// Updates the planning approval status to 'Waiting for Approval'.
    /// </summary>
    /// <param name="request">The request containing the customer ID and planning control ID.</param>
    /// <returns>An HTTP 200 OK response if successful, or an HTTP 400 Bad Request response if the request is invalid.</returns>
    [HttpPost(ApiRoutes.PlanningApprovals.WaitingForApproval)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> WaitingForApproval([FromBody] WaitingApprovalStatusRequest request)
        => await Result.Create(request, DomainErrors.General.UnProcessableRequest)
            .Map(rq => new UpdateWaitingForApproveStatusCommand(rq.CustomerId, rq.PlanningControlId))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);

    /// <summary>
    /// Updates the planning approval status to 'Approved'.
    /// </summary>
    /// <param name="planningApprovalId">The ID of the planning approval to be approved.</param>
    /// <returns>An HTTP 200 OK response if successful, or an HTTP 400 Bad Request response if the request is invalid.</returns>
    [HttpPut(ApiRoutes.PlanningApprovals.Approve)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Approve(Guid planningApprovalId)
        => await Result.Create(new { planningApprovalId }, DomainErrors.General.UnProcessableRequest)
            .Map(request => new UpdateApprovedStatusCommand(request.planningApprovalId))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);

    /// <summary>
    /// Updates the planning approval status to 'Edit Request'.
    /// </summary>
    /// <param name="planningApprovalId">The ID of the planning approval to request an edit.</param>
    /// <returns>An HTTP 200 OK response if successful, or an HTTP 400 Bad Request response if the request is invalid.</returns>
    [HttpPut(ApiRoutes.PlanningApprovals.EditRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> EditRequest(Guid planningApprovalId)
        => await Result.Create(new { planningApprovalId }, DomainErrors.General.UnProcessableRequest)
            .Map(request => new UpdateEditRequestStatusCommand(request.planningApprovalId))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);

    /// <summary>
    /// Updates the planning approval status to 'Unlock'.
    /// </summary>
    /// <param name="planningApprovalId">The ID of the planning approval to be unlocked.</param>
    /// <returns>An HTTP 200 OK response if successful, or an HTTP 400 Bad Request response if the request is invalid.</returns>
    [HttpPut(ApiRoutes.PlanningApprovals.Unlock)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Unlock(Guid planningApprovalId)
        => await Result.Create(new { planningApprovalId }, DomainErrors.General.UnProcessableRequest)
            .Map(request => new UpdateUnlockStatusCommand(request.planningApprovalId))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);

    /// <summary>
    /// Retrieves planning approval statistics by sales based on the provided request.
    /// </summary>
    /// <param name="request">The request containing the planning control ID, managed by user IDs, and whether to include subordinate users.</param>
    /// <returns>A list of <see cref="PlanningApprovalStatisticsBySalesResponse"/> if successful, or an <see cref="ApiErrorResponse"/> if the request is invalid.</returns>
    [HttpPost(ApiRoutes.PlanningApprovals.DashboardBySales)]
    [ProducesResponseType(typeof(List<PlanningApprovalStatisticsBySalesResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DashboardBySales([FromBody] PlanningApprovalStatisticsBySalesRequest request)
        => await Maybe<PlanningApprovalStatisticsBySalesQuery>
            .From(new PlanningApprovalStatisticsBySalesQuery(request.PlanningControlId, request.PlanningApprovalStatuses, request.ManagedByUserIds, request.IncludeSubordinateUsers))
            .Bind(query => Mediator.Send(query))
            .Match(Ok, BadRequest);
}