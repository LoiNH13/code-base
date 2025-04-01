using ApiService.Contracts;
using ApiService.Infrastructure;
using Contract.Common;
using Domain.Core.Errors;
using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sms.ApiService.Contracts;
using Sms.Application.ResMos.Commands.Create;
using Sms.Application.ResMos.Queries.ResMos;
using Sms.Contract.ResMos;

namespace Sms.ApiService.Controllers;

/// <summary>
///     Controller for handling ResMo (Received Message) operations.
/// </summary>
/// <param name="mediator">The mediator instance for handling command and query operations.</param>
[AllowAnonymous]
public class ResMosController(IMediator mediator) : ApiController(mediator)
{
    /// <summary>
    ///     Retrieves a paginated list of ResMo (Received Message) responses.
    /// </summary>
    /// <param name="pageNumber">The number of the page to retrieve. This value is used for pagination.</param>
    /// <param name="pageSize">The number of items per page. This value is used for pagination.</param>
    /// <param name="servicePhone">The phone number associated with the service.</param>
    /// <returns>
    ///     An IActionResult that represents either:
    ///     - A 200 OK response with a List ResMoResponse if the operation is successful.
    ///     The PagedList ResMoResponse contains the paginated list of ResMo responses.
    ///     - A 400 Bad Request response with an ApiErrorResponse if the operation fails.
    ///     The ApiErrorResponse contains details about the error that occurred.
    /// </returns>
    [HttpGet(ApiRoutes.ResMos.Get)]
    [ProducesResponseType(typeof(PagedList<ResMoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get(int pageNumber, int pageSize, string? servicePhone)
    {
        return await Maybe<ResMosQuery>.From(new ResMosQuery(pageNumber, pageSize, servicePhone))
            .Bind(query => Mediator.Send(query))
            .Match(Ok, BadRequest);
    }

    /// <summary>
    ///     Creates a new ResMo (Received Message) entry.
    /// </summary>
    /// <param name="request">The request object containing the details for the new ResMo.</param>
    /// <returns>
    ///     An IActionResult that represents either:
    ///     - A 200 OK response if the creation is successful.
    ///     - A 400 Bad Request response with an ApiErrorResponse if the creation fails.
    /// </returns>
    [HttpPost(ApiRoutes.ResMos.Create)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateResMoRequest request)
    {
        return await Result.Create(request, DomainErrors.General.UnProcessableRequest)
            .Map(req => new CreateResMoCommand(req.ServicePhone, req.PricePerMo, req.FreeMtPerMo))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);
    }
}