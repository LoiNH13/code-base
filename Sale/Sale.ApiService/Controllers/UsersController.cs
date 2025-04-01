using Contract.Common;
using Domain.Core.Errors;
using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using Microsoft.AspNetCore.Mvc;
using Sale.ApiService.Contracts;
using Sale.Application.Users.Commands.Update;
using Sale.Application.Users.Queries.UserById;
using Sale.Application.Users.Queries.Users;
using Sale.Application.Users.Queries.UsersTree;
using Sale.Application.Users.Queries.UsersWithPlanApproval;
using Sale.Contract.Users;

namespace Sale.ApiService.Controllers;

public class UsersController(IMediator mediator) : ApiController(mediator)
{
    /// <summary>
    /// Retrieves a paginated list of users based on the provided parameters.
    /// </summary>
    /// <param name="pageNumber">The page number to retrieve.</param>
    /// <param name="pageSize">The number of users per page.</param>
    /// <param name="searchText">An optional search text to filter users by name.</param>
    /// <returns>A paginated list of UserResponse objects.</returns>
    [HttpGet(ApiRoutes.Users.Get)]
    [ProducesResponseType(typeof(PagedList<UserResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUsers(int pageNumber, int pageSize, string? searchText) =>
        await Maybe<UsersQuery>.From(new UsersQuery(pageNumber, pageSize, searchText))
            .Bind(query => Mediator.Send(query))
            .Match(Ok, BadRequest);

    /// <summary>
    /// Retrieves a list of users in a hierarchical tree structure.
    /// </summary>
    /// <param name="managedByUserId">An optional user ID to filter users who are managed by the specified user.</param>
    /// <returns>A list of UserResponse objects representing the hierarchical tree structure.</returns>
    [HttpGet(ApiRoutes.Users.GetTree)]
    [ProducesResponseType(typeof(List<UserResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUsersTree(Guid? managedByUserId) =>
        await Maybe<UsersTreeQuery>.From(new UsersTreeQuery(managedByUserId))
            .Bind(query => Mediator.Send(query))
            .Match(Ok, BadRequest);

    /// <summary>
    /// Retrieves a list of users based on the provided plan approval request parameters.
    /// </summary>
    /// <param name="request">The UsersWithPlanApprovalRequest object containing the request parameters.</param>
    /// <returns>A list of UsersWithPlanApprovalResponse objects.</returns>
    [HttpPost(ApiRoutes.Users.GetWithPlan)]
    [ProducesResponseType(typeof(List<UsersWithPlanApprovalResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUsersWithPlan([FromBody] UsersWithPlanApprovalRequest request) =>
        await Maybe<UsersWithPlanApprovalQuery>.From(new UsersWithPlanApprovalQuery(request.PlanningControlId, request.Status, request.IsHaveOdoo, request.ManagedByUserIds, true))
            .Bind(query => Mediator.Send(query))
            .Match(Ok, BadRequest);

    /// <summary>
    /// Retrieves a single user by their unique identifier.
    /// </summary>
    /// <param name="userId">The unique identifier of the user to retrieve.</param>
    /// <returns>A UserResponse object representing the user.</returns>
    [HttpGet(ApiRoutes.Users.GetById)]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUser(Guid userId) =>
    await Maybe<UserByIdQuery>.From(new UserByIdQuery(userId))
        .Bind(query => Mediator.Send(query))
        .Match(Ok, NotFound);

    /// <summary>
    /// Updates an existing user with the provided information.
    /// </summary>
    /// <param name="userId">The unique identifier of the user to update.</param>
    /// <param name="request">The UpdateUserRequest object containing the updated information.</param>
    /// <returns>An HTTP 200 OK response if the update is successful.</returns>
    [HttpPut(ApiRoutes.Users.Update)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateUser(Guid userId, [FromBody] UpdateUserRequest request) =>
    await Result.Create(request, DomainErrors.General.UnProcessableRequest)
        .Map(request => new UpdateUserCommand(userId,
                                              request.Name,
                                              request.ManagedByUserId,
                                              request.Role,
                                              request.BusinessType))
        .Bind(command => Mediator.Send(command))
        .Match(Ok, BadRequest);
}
