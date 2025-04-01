using Domain.Core.Primitives.Result;
using Odoo.Contract.Services.AccountMoves;
using Odoo.Contract.Services.Payments;
using Odoo.Contract.Services.SaleOrders;

namespace Odoo.Application.Core.Odoo
{
    public interface IOdooServices
    {
        Task<Result> GetPartner();

        Task<Result> CreatePartner();

        Task<Result> UpdatePartner();

        Task<Result<SaleOrderResponse>> GetSaleOrder(string refId);

        Task<Result> CreateSaleOrder();

        Task<Result<ConfirmInvoiceResponse>> ConfirmInvoice(ConfirmInvoiceServiceRequest request);

        Task<Result<CreateOrUpdatePaymentResponse>> CreateOrUpdatePayment(CreateOrUpdatePaymentRequest request);

        Task<Result<AccountMoveResponse>> CreateAccountMove(CreateAccountMoveRequest request);

        Task<Result<AccountMoveResponse>> CancelAccountMove(int id);

    }
}