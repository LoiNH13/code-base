using Application.Core.Abstractions.Messaging;
using Domain.Core.Primitives.Result;
using Odoo.Contract.Services.AccountMoves;

namespace Odoo.Application.Invoices.Updates.Confirm
{
    public sealed class ConfirmInvoiceCommand : ICommand<Result<ConfirmInvoiceResponse>>
    {
        public ConfirmInvoiceCommand(string origin, string companyAnalyticAccount, string confirmMethod)
        {
            Origin = origin;
            CompanyAnalyticAccount = companyAnalyticAccount;
            ConfirmMethod = confirmMethod;
        }

        public string Origin { get; }

        public string CompanyAnalyticAccount { get; }

        public string ConfirmMethod { get; }
    }
}
