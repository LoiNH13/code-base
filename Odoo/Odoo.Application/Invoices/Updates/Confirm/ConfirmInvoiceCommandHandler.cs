using Application.Core.Abstractions.Messaging;
using Domain.Core.Primitives.Result;
using Odoo.Application.Core.Odoo;
using Odoo.Contract.Services.AccountMoves;

namespace Odoo.Application.Invoices.Updates.Confirm
{
    internal sealed class ConfirmInvoiceCommandHandler : ICommandHandler<ConfirmInvoiceCommand, Result<ConfirmInvoiceResponse>>
    {
        readonly IOdooServices _odooServices;

        public ConfirmInvoiceCommandHandler(IOdooServices odooServices) => _odooServices = odooServices;

        public async Task<Result<ConfirmInvoiceResponse>> Handle(ConfirmInvoiceCommand request, CancellationToken cancellationToken)
            => await _odooServices.ConfirmInvoice(new ConfirmInvoiceServiceRequest
            {
                CompanyAnalyticAccount = request.CompanyAnalyticAccount,
                Origin = request.Origin,
                ConfirmMethod = request.ConfirmMethod
            });
    }

}
