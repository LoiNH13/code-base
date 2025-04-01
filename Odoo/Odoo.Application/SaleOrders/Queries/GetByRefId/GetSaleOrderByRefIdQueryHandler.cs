using Application.Core.Abstractions.Messaging;
using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using Odoo.Application.Core.Odoo;
using Odoo.Contract.Services.SaleOrders;
using Odoo.Domain.Repositories;

namespace Odoo.Application.SaleOrders.Queries.GetByRefId
{
    internal sealed class GetSaleOrderByRefIdQueryHandler : IQueryHandler<GetSaleOrderByRefIdQuery, Maybe<SaleOrderResponse>>
    {
        readonly IOdooServices _odooServices;
        readonly IOdooOrderRepository _odooOrderRepository;

        public GetSaleOrderByRefIdQueryHandler(IOdooServices odooServices, IOdooOrderRepository odooOrderRepository)
        {
            _odooServices = odooServices;
            _odooOrderRepository = odooOrderRepository;
        }

        public async Task<Maybe<SaleOrderResponse>> Handle(GetSaleOrderByRefIdQuery request, CancellationToken cancellationToken)
        {
            var dta = await _odooOrderRepository.SaleReportsByMonth(41994, 24303);
            if (dta == null) return default!;

            Result<SaleOrderResponse> rsSaleOrder = await _odooServices.GetSaleOrder(request.RefId);
            if (rsSaleOrder.IsSuccess) return rsSaleOrder.Value;
            else
            {
                return Maybe<SaleOrderResponse>.None;
            }
        }
    }
}
