using Contract.Common;
using Domain.Core.Errors;
using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sale.ApiService.Contracts;
using Sale.Application.Customers.Commands.Creates.Odoo;
using Sale.Application.Customers.Commands.Creates.Plan;
using Sale.Application.Customers.Commands.Delete;
using Sale.Application.Customers.Commands.Updates.ManagedCustomers;
using Sale.Application.Customers.Commands.Updates.MappingOdoo;
using Sale.Application.Customers.Commands.Updates.Update;
using Sale.Application.Customers.Queries.CustomerById;
using Sale.Application.Customers.Queries.Customers;
using Sale.Application.Customers.Queries.CustomersWithPlanApprovals.Paging;
using Sale.Application.Customers.Queries.CustomersWithPlanApprovals.Summary;
using Sale.Contract.Customers;
using Sale.Domain.Enumerations;

namespace Sale.ApiService.Controllers;

public class CustomersController(IMediator mediator) : ApiController(mediator)
{
    /// <summary>
    /// Retrieves a paged list of customers based on specified criteria.
    /// </summary>
    /// <param name="pageNumber">The page number for pagination.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <param name="userId">Optional. The ID of the user to filter customers by.</param>
    /// <param name="searchText">Optional. The text to search for in customer data.</param>
    /// <param name="customerTag">Optional. Filter customers by tag.</param>
    /// <param name="notHaveManaged">Optional. Filter customers by whether they are not managed.</param>
    /// <returns>An ActionResult containing a paged list of CustomerResponse objects if successful, or a BadRequest result if the request is invalid.</returns>
    [HttpGet(ApiRoutes.Customers.Get)]
    [ProducesResponseType(typeof(PagedList<CustomerResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCustomers(int pageNumber, int pageSize, Guid? userId, string? searchText,
        ECustomerTag? customerTag, bool? notHaveManaged)
        => await Maybe<CustomerQuery>
            .From(new CustomerQuery(pageNumber, pageSize, userId, searchText, customerTag, notHaveManaged))
            .Bind(query => Mediator.Send(query))
            .Match(Ok, BadRequest);

    /// <summary>
    /// Retrieves a paged list of customers with their associated plans.
    /// </summary>
    /// <param name="request">The CustomerWithPlanRequest object containing filter and pagination parameters.</param>
    /// <returns>An ActionResult containing a paged list of CustomerWithPlanResponse objects if successful, or a BadRequest result if the request is invalid.</returns>
    [HttpPost(ApiRoutes.Customers.GetWithPlan)]
    [ProducesResponseType(typeof(PagedList<CustomerWithPlanResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCustomersWithPlan([FromBody] CustomerWithPlanRequest request) =>
        await Maybe<PagingCustomersWithPlanQuery>.From(new PagingCustomersWithPlanQuery(request.PlanningControlId,
                request.PageNumber,
                request.PageSize,
                request.ManagedByUserId,
                request.PlanningApprovalStatus,
                request.SearchText,
                request.ECustomerTag))
            .Bind(query => Mediator.Send(query))
            .Match(Ok, BadRequest);

    /// <summary>
    /// Retrieves a summary of customers with their associated plans.
    /// </summary>
    /// <param name="request">The SummaryCustomersWithPlanRequest object containing filter parameters.</param>
    /// <returns>An ActionResult containing a list of SummaryCustomersWithPlanResponse objects if successful, or a BadRequest result if the request is invalid.</returns>
    [HttpPost(ApiRoutes.Customers.GetWithPlanSummary)]
    [ProducesResponseType(typeof(List<SummaryCustomersWithPlanResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCustomersWithPlanSummary([FromBody] SummaryCustomersWithPlanRequest request) =>
        await Maybe<SummaryCustomersWithPlanQuery>.From(new SummaryCustomersWithPlanQuery(request.PlanningControlId,
                request.Status,
                request.SearchText,
                request.ManagedByUserId,
                request.CustomerTag))
            .Bind(query => Mediator.Send(query))
            .Match(Ok, BadRequest);

    /// <summary>
    /// Retrieves a customer by their unique identifier.
    /// </summary>
    /// <param name="customerId">The unique identifier of the customer.</param>
    /// <returns>An ActionResult containing the CustomerResponse if found, or a NotFound result if the customer doesn't exist.</returns>
    [HttpGet(ApiRoutes.Customers.GetById)]
    [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCustomerById(Guid customerId) =>
        await Maybe<CustomerByIdQuery>.From(new CustomerByIdQuery(customerId))
            .Bind(query => Mediator.Send(query))
            .Match(Ok, NotFound);

    /// <summary>
    /// Creates a new Odoo customer.
    /// </summary>
    /// <param name="request">The CreateOdooCustomerRequest object containing the new customer's details.</param>
    /// <returns>An ActionResult indicating success or failure of the operation.</returns>
    [Authorize(Roles = "Admin")]
    [HttpPost(ApiRoutes.Customers.OdooCreate)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateOdooCustomer([FromBody] CreateOdooCustomerRequest request) =>
        await Result.Create(request, DomainErrors.General.UnProcessableRequest)
            .Map(rq => new CreateOdooCustomerCommand(rq.OdooRef, rq.VisitPerMonth, rq.ManagedByUserId, rq.BusinessType))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);

    /// <summary>
    /// Creates a new plan customer.
    /// </summary>
    /// <param name="request">The CreatePlanCustomerRequest object containing the new customer's plan details.</param>
    /// <returns>An ActionResult indicating success or failure of the operation.</returns>
    [HttpPost(ApiRoutes.Customers.PlanCreate)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreatePlanCustomer([FromBody] CreatePlanCustomerRequest request) =>
        await Result.Create(request, DomainErrors.General.UnProcessableRequest)
            .Map(rq => new CreatePlanCustomerCommand(rq.CityId, rq.DistrictId, rq.WardId))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);

    /// <summary>
    /// Updates an existing customer's information.
    /// </summary>
    /// <param name="customerId">The unique identifier of the customer to update.</param>
    /// <param name="request">The UpdateCustomerRequest object containing the updated customer information.</param>
    /// <returns>An ActionResult indicating success or failure of the operation.</returns>
    [HttpPut(ApiRoutes.Customers.Update)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCustomer(Guid customerId, [FromBody] UpdateCustomerRequest request) =>
        await Result.Create(request, DomainErrors.General.UnProcessableRequest)
            .Map(rq => new UpdateCustomerCommand(customerId, rq.ManagedByUserId, rq.VitsitPerMonth, rq.BusinessType))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);

    /// <summary>
    /// Updates the managed-by user for multiple customers.
    /// </summary>
    /// <param name="request">The UpdateManagedCustomersRequest object containing the user ID and list of customer IDs to update.</param>
    /// <returns>An ActionResult indicating success or failure of the operation.</returns>
    [Authorize(Roles = "Admin")]
    [HttpPost(ApiRoutes.Customers.UpdateManagedBy)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCustomerManagedBy([FromBody] UpdateManagedCustomersRequest request) =>
        await Result.Create(request, DomainErrors.General.UnProcessableRequest)
            .Map(rq => new UpdateManagedCustomersCommand(rq.UserId, rq.CustomerIds))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);

    /// <summary>
    /// Maps an existing customer to an Odoo customer.
    /// </summary>
    /// <param name="customerId">The unique identifier of the customer to map.</param>
    /// <param name="request">The MappingOdooCustomerRequest object containing the Odoo reference details.</param>
    /// <returns>An ActionResult indicating success or failure of the operation.</returns>
    [Authorize(Roles = "Admin")]
    [HttpPut(ApiRoutes.Customers.MappingOdoo)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> MappingOdooCustomer(Guid customerId, [FromBody] MappingOdooCustomerRequest request) =>
        await Result.Create(request, DomainErrors.General.UnProcessableRequest)
            .Map(rq => new UpdatePlanningCustomerMappingOdooCommand(customerId, rq.OdooRef))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);

    /// <summary>
    /// Deletes an existing customer.
    /// </summary>
    /// <param name="customerId">The unique identifier of the customer to delete.</param>
    /// <returns>An ActionResult indicating success or failure of the operation.</returns>
    [Authorize(Roles = "Admin")]
    [HttpDelete(ApiRoutes.Customers.Delete)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteCustomer(Guid customerId) =>
        await Result.Create(new { customerId }, DomainErrors.General.UnProcessableRequest)
            .Map(rq => new DeleteCustomerCommand(rq.customerId))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);
}