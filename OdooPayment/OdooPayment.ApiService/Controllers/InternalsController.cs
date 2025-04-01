using ApiService.Contracts;
using ApiService.Infrastructure;
using Domain.Core.Errors;
using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OdooPayment.ApiService.Contracts;
using OdooPayment.Application.Customers.Queries.Odoo.Customers;
using OdooPayment.Application.Payments.Commands.Odoos.Create;
using OdooPayment.Application.Payments.Commands.PaymentSms.Updates.Confirm;
using OdooPayment.Contract.OdooCustomers;
using OdooPayment.Contract.OdooPayments;
using OdooPayment.Contract.PaymentSms;

namespace OdooPayment.ApiService.Controllers
{
    [AllowAnonymous]
    public class InternalsController(IMediator mediator) : ApiController(mediator)
    {
        [HttpPost(ApiRouters.Internals.CreateOdooPayment)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOdooPayment([FromBody] CreateOdooPaymentRequest request) => await
            Result.Create(request, DomainErrors.General.UnProcessableRequest)
                .Map(req => new CreateOdooPaymentByPaymentSmsIdCommand(req.PaymentSmsId))
                .Bind(command => Mediator.Send(command))
                .Match(Ok, BadRequest);

        [HttpPost(ApiRouters.Internals.ConfirmPaymentSms)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ConfirmPaymentSms([FromBody] ConfirmPaymentSmsRequest request) => await
            Result.Create(request, DomainErrors.General.UnProcessableRequest)
                .Map(req => new ConfirmPaymentSmsCommand(req.PaymentSmsId))
                .Bind(command => Mediator.Send(command))
                .Match(Ok, BadRequest);

        [HttpGet(ApiRouters.Internals.Customers)]
        [ProducesResponseType(typeof(CustomerOdooModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomers([FromQuery] string search,
                                                      [FromQuery] string invoiceSearch,
                                                      [FromQuery] bool isSO,
                                                      [FromQuery] bool isCustomer) =>
            await Maybe<GetOdooCustomerQuery>.From(new GetOdooCustomerQuery(search, invoiceSearch, isSO, isCustomer))
                .Bind(query => Mediator.Send(query))
                .Match(Ok, NotFound);

    }
}
