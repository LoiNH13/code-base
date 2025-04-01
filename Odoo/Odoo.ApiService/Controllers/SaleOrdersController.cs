using ApiService.Infrastructure;
using Domain.Core.Primitives.Maybe;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Odoo.ApiService.Contracts;
using Odoo.Application.SaleOrders.Queries.GetByRefId;
using Odoo.Contract.Services.SaleOrders;

namespace Odoo.ApiService.Controllers
{
    [AllowAnonymous]
    public class SaleOrdersController(IMediator mediator) : ApiController(mediator)
    {
        [HttpGet(ApiRouters.SaleOrders.GetByRefId)]
        [ProducesResponseType(typeof(SaleOrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByRefId(string refId) =>
            await Maybe<GetSaleOrderByRefIdQuery>.From(new GetSaleOrderByRefIdQuery(refId))
                .Bind(query => Mediator.Send(query))
                .Match(Ok, NotFound);
    }
}
