using Application.Core.Abstractions.Messaging;
using Domain.Core.Primitives.Maybe;
using Odoo.Contract.Services.SaleOrders;

namespace Odoo.Application.SaleOrders.Queries.GetByRefId
{
    public sealed class GetSaleOrderByRefIdQuery : IQuery<Maybe<SaleOrderResponse>>
    {
        public GetSaleOrderByRefIdQuery(string refId) => RefId = refId;
        public string RefId { get; }
    }
}
