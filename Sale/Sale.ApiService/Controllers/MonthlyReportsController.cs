using Contract.Common;
using Domain.Core.Errors;
using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using Microsoft.AspNetCore.Mvc;
using Sale.ApiService.Contracts;
using Sale.Application.MonthlyReports.Commands.Create;
using Sale.Application.MonthlyReports.Commands.Updates.Confirm;
using Sale.Application.MonthlyReports.Commands.Updates.Update;
using Sale.Application.MonthlyReports.Queries.Dashboards.CustomerTracking;
using Sale.Application.MonthlyReports.Queries.Dashboards.MonthlyReportsBySales;
using Sale.Application.MonthlyReports.Queries.MonthlyReportById;
using Sale.Application.MonthlyReports.Queries.MonthlyReportByYearMonth;
using Sale.Application.MonthlyReports.Queries.MonthlyReports;
using Sale.Contract.Customers;
using Sale.Contract.MonthlyReports;
using Sale.Domain.ValueObjects;

namespace Sale.ApiService.Controllers;

public class MonthlyReportsController(IMediator mediator) : ApiController(mediator)
{
    /// <summary>
    /// Retrieves a paginated list of monthly reports based on the provided parameters.
    /// </summary>
    /// <param name="pageNumber">The page number to retrieve.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <param name="createdBy">Optional parameter to filter reports by the creator's ID.</param>
    /// <param name="customerId">customerId</param>
    /// <returns>A paginated list of monthly reports or an error response if the request is invalid.</returns>
    [HttpGet(ApiRoutes.MonthlyReports.Get)]
    [ProducesResponseType(typeof(PagedList<MonthlyReportResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetMonthlyReports(int pageNumber, int pageSize, Guid? createdBy, Guid? customerId) =>
        await Maybe<MonthlyReportsQuery>
            .From(new MonthlyReportsQuery(pageNumber, pageSize, createdBy, customerId))
            .Bind(query => Mediator.Send(query))
            .Match(Ok, BadRequest);

    /// <summary>
    /// Retrieves a list of monthly reports by sales based on the provided request data.
    /// </summary>
    /// <param name="request">The request data containing the details for retrieving monthly reports by sales.</param>
    /// <returns>A list of monthly reports by sales or an error response if the request is invalid.</returns>
    [HttpPost(ApiRoutes.MonthlyReports.DashboardMonthlyReportBySales)]
    [ProducesResponseType(typeof(List<MonthlyReportsBySaleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetMonthlyReportsBySales([FromBody] MonthlyReportsBySalesRequest request) =>
        await Maybe<MonthlyReportsBySalesQuery>
            .From(new MonthlyReportsBySalesQuery(request.ConvertMonths, request.ManagedByUserIds, request.IncludeSubordinateUsers))
            .Bind(query => Mediator.Send(query))
            .Match(Ok, BadRequest);

    /// <summary>
    /// Retrieves customer tracking information based on the provided request data.
    /// </summary>
    /// <param name="request">The request data containing the details for retrieving customer tracking information.</param>
    /// <returns>A list of customer responses or an error response if the request is invalid.</returns>
    [HttpPost(ApiRoutes.MonthlyReports.DashboardCustomerTracking)]
    [ProducesResponseType(typeof(List<CustomerResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCustomerTracking([FromBody] CustomerTrackingRequest request) =>
       await Maybe<CustomerTrackingQuery>
           .From(new CustomerTrackingQuery(request.ConvertMonths, request.PageNumber, request.PageSize, request.ManagedByUserIds, request.IncludeSubordinateUsers, request.IsVisited))
           .Bind(query => Mediator.Send(query))
           .Match(Ok, BadRequest);

    /// <summary>
    /// Retrieves a specific monthly report by its ID.
    /// </summary>
    /// <param name="monthlyReportId">The ID of the monthly report to retrieve.</param>
    /// <returns>The requested monthly report or a 404 Not Found status if the report does not exist.</returns>
    [HttpGet(ApiRoutes.MonthlyReports.GetById)]
    [ProducesResponseType(typeof(MonthlyReportResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMonthlyReportById(Guid monthlyReportId) =>
        await Maybe<MonthlyReportByIdQuery>.From(new MonthlyReportByIdQuery(monthlyReportId))
            .Bind(query => Mediator.Send(query))
            .Match(Ok, BadRequest);

    /// <summary>
    /// Creates a new monthly report based on the provided request data.
    /// </summary>
    /// <param name="request">The request data containing the details of the new monthly report.</param>
    /// <returns>The created monthly report or an error response if the request is invalid.</returns>
    [HttpPost(ApiRoutes.MonthlyReports.Create)]
    [ProducesResponseType(typeof(CreateMonthlyReportResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateMonthlyReport([FromBody] CreateMonthlyReportRequest request) =>
        await Result.Create(request, DomainErrors.General.UnProcessableRequest)
            .Map(rq => new CreateMonthlyReportCommand(rq.FromTimeOnUtc,
                rq.ToTimeOnUtc,
                new DealerMonthlyReport(rq.DailyVisitors, rq.DailyPurchases, rq.OnlinePurchaseRate),
                rq.Note,
                rq.OdooCustomerId,
                rq.Items))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);

    /// <summary>
    /// Creates a new monthly report for a manufacturer based on the provided request data.
    /// </summary>
    /// <param name="request">The request data containing the details of the new monthly report for the manufacturer.</param>
    /// <returns>The created monthly report or an error response if the request is invalid.</returns>
    [HttpPost(ApiRoutes.MonthlyReports.CreateForManufacturer)]
    [ProducesResponseType(typeof(CreateMonthlyReportResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateMonthlyReportForManufacturer([FromBody] CreateManufacturerMonthlyReportRequest request) =>
        await Result.Create(request, DomainErrors.General.UnProcessableRequest)
            .Map(rq => new CreateMonthlyReportCommand(rq.FromTimeOnUtc,
                rq.ToTimeOnUtc,
                new ManufacturerMonthlyReport(rq.NewBuyInMonth, rq.NewBuyNextMonth, rq.ServiceInMonth),
                rq.Note,
                rq.OdooCustomerId,
                rq.Items))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);

    /// <summary>
    /// Updates an existing monthly report with the provided request data.
    /// </summary>
    /// <param name="monthlyReportId">The ID of the monthly report to update.</param>
    /// <param name="request">The request data containing the updated details of the monthly report.</param>
    /// <returns>A 200 OK status if the update is successful or an error response if the request is invalid.</returns>
    [HttpPut(ApiRoutes.MonthlyReports.Update)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateMonthlyReport(Guid monthlyReportId,
        [FromBody] UpdateMonthlyReportRequest request)
    {
        return await Result.Create(request, DomainErrors.General.UnProcessableRequest)
            .Map(rq => new UpdateMonthlyReportCommand(monthlyReportId,
                rq.FromTimeOnUtc,
                rq.ToTimeOnUtc,
                new DealerMonthlyReport(rq.DailyVisitors, rq.DailyPurchases, rq.OnlinePurchaseRate),
                rq.Note,
                rq.Items))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);
    }

    /// <summary>
    /// Updates an existing monthly report for a manufacturer with the provided request data.
    /// </summary>
    /// <param name="monthlyReportId">The ID of the monthly report to update.</param>
    /// <param name="request">The request data containing the updated details of the monthly report for the manufacturer.</param>
    /// <returns>A 200 OK status if the update is successful or an error response if the request is invalid.</returns>
    [HttpPut(ApiRoutes.MonthlyReports.UpdateForManufacturer)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateMonthlyReportForManufacturer(Guid monthlyReportId,
        [FromBody] UpdateManufacturerMonthlyReportRequest request)
    {
        return await Result.Create(request, DomainErrors.General.UnProcessableRequest)
            .Map(rq => new UpdateMonthlyReportCommand(monthlyReportId,
                rq.FromTimeOnUtc,
                rq.ToTimeOnUtc,
                new ManufacturerMonthlyReport(rq.NewBuyInMonth, rq.NewBuyNextMonth, rq.ServiceInMonth),
                rq.Note,
                rq.Items))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);
    }

    /// <summary>
    /// Confirms a monthly report as valid and ready for further processing.
    /// </summary>
    /// <param name="monthlyReportId">The ID of the monthly report to confirm.</param>
    /// <returns>A 200 OK status if the confirmation is successful or an error response if the request is invalid.</returns>
    [HttpPut(ApiRoutes.MonthlyReports.Confirm)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ConfirmMonthlyReport(Guid monthlyReportId) =>
        await Result.Create(new { monthlyReportId }, DomainErrors.General.UnProcessableRequest)
            .Map(request => new ConfirmMonthlyReportCommand(request.monthlyReportId))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);

    /// <summary>
    /// Retrieves a monthly report for a specific year and month.
    /// </summary>
    /// <param name="request">The request data containing the customer IDs, year, and month for the report.</param>
    /// <returns>The requested monthly report or a 404 Not Found status if the report does not exist.</returns>
    [HttpPost(ApiRoutes.MonthlyReports.GetByYearMonth)]
    [ProducesResponseType(typeof(List<CustomerResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMonthlyReportByYearMonth([FromBody] MonthlyReportByYearMonthRequest request) =>
        await Maybe<MonthlyReportByYearMonthQuery>
            .From(new MonthlyReportByYearMonthQuery(request.CustomerIds, request.Year, request.Month))
            .Bind(query => Mediator.Send(query))
            .Match(Ok, NotFound);


}