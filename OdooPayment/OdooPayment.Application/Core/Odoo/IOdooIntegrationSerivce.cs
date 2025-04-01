using Domain.Core.Primitives.Result;
using Payment.Persistence.Models;

namespace OdooPayment.Application.Core.Odoo
{
    public interface IOdooIntegrationSerivce
    {
        Task<Result> CreateOdooPayment(PaymentSm paymentSms);
    }
}
