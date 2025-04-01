using ApiService.Infrastructure;
using Domain.Core.Errors;
using Domain.Core.Primitives.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Odoo.ApiService.Contracts;
using Odoo.Application.Invoices.Updates.Confirm;
using Odoo.Contract.Invoices;
using Odoo.Contract.Services.AccountMoves;

namespace Odoo.ApiService.Controllers
{
    [AllowAnonymous]
    public class InvoicesController(IMediator mediator) : ApiController(mediator)
    {
        [HttpPost(ApiRouters.Invoices.Confirm)]
        [ProducesResponseType(typeof(ConfirmInvoiceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Confirm([FromBody] ConfirmInvoiceRequest request) =>
            await Result.Create(request, DomainErrors.General.UnProcessableRequest)
            .Map(req => new ConfirmInvoiceCommand(req.Origin, req.CompanyAnalyticAccount, req.ConfirmMethod))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);
    }
}
