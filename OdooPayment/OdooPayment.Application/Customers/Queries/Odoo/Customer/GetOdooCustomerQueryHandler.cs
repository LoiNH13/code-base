using Application.Core.Abstractions.Messaging;
using Application.Core.Extensions;
using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using Microsoft.EntityFrameworkCore;
using Odoo.Domain.Entities;
using Odoo.Domain.Repositories;
using Odoo.Domain.ValueObjects;
using OdooPayment.Contract.OdooCustomers;

namespace OdooPayment.Application.Customers.Queries.Odoo.Customers
{
    internal sealed class GetOdooCustomerQueryHandler : IQueryHandler<GetOdooCustomerQuery, Maybe<CustomerOdooModel>>
    {
        readonly IOdooCustomerRepository _odooCustomerRepository;
        readonly IOdooOrderRepository _odooOrderRepository;

        public GetOdooCustomerQueryHandler(IOdooCustomerRepository odooCustomerRepository, IOdooOrderRepository odooOrderRepository)
        {
            _odooCustomerRepository = odooCustomerRepository;
            _odooOrderRepository = odooOrderRepository;
        }

        public async Task<Maybe<CustomerOdooModel>> Handle(GetOdooCustomerQuery request, CancellationToken cancellationToken)
        {
            if (request.IsSO ?? false)
            {
                var mbSo = await _odooOrderRepository.NotPaidSOViewByInvoices(request.Search, request.InvoiceSearch?.Split(",").ToList());
                if (!mbSo.Exists(x => !string.IsNullOrWhiteSpace(x.InternalRef))) return Maybe<CustomerOdooModel>.None;

                var internalRef = mbSo.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x.InternalRef))?.InternalRef ?? request.Search;
                Maybe<VietmapCustomerView> mbCustomer = await _odooCustomerRepository.ViewQueryable()
                    .Where(x => x.InternalRef == internalRef)
                    .FirstOrDefaultAsync(cancellationToken) ?? default!;
                if (mbCustomer.HasNoValue) return Maybe<CustomerOdooModel>.None;

                return CustomerOdooModel.Create(mbCustomer.Value, mbSo);
            }
            else if ((request.IsCustomer ?? false) && !string.IsNullOrWhiteSpace(request.Search))
            {
                Result<OrderCode> rsOrderCode = OrderCode.Create(request.Search);
                var saleOrders = await _odooOrderRepository.ViewQueryable()
                    .WhereIf(rsOrderCode.IsSuccess, x => x.InternalRef == request.Search!.ToUpper() || x.OrderName == rsOrderCode.Value)
                    .WhereIf(rsOrderCode.IsFailure, x => x.InternalRef == request.Search!.ToUpper())
                    .ToListAsync(cancellationToken);

                Maybe<VietmapCustomerView> mbCustomer = await _odooCustomerRepository.ViewQueryable()
                    .Where(x => x.InternalRef == (saleOrders.Any() ? saleOrders.FirstOrDefault()!.InternalRef : request.Search))
                    .FirstOrDefaultAsync() ?? default!;
                if (mbCustomer.HasNoValue) return Maybe<CustomerOdooModel>.None;

                return CustomerOdooModel.Create(mbCustomer.Value, saleOrders);
            }

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                Maybe<VietmapCustomerView> mbCustomer = await _odooCustomerRepository.ViewQueryable()
                    .Where(x => x.InternalRef == request.Search || x.Id.ToString() == request.Search || x.Mobile == request.Search)
                    .FirstOrDefaultAsync() ?? default!;
                return mbCustomer.HasValue ? new CustomerOdooModel(mbCustomer) : Maybe<CustomerOdooModel>.None;
            }
            return Maybe<CustomerOdooModel>.None;
        }
    }
}
