using Application.Core.Abstractions.Messaging;
using Domain.Core.Primitives;
using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using Microsoft.EntityFrameworkCore;
using OdooPayment.Application.Core.Odoo;
using Payment.Persistence.Models;

namespace OdooPayment.Application.Payments.Commands.Odoos.Create
{
    internal sealed class CreateOdooPaymentByPaymentSmsIdCommandHandler :
        ICommandHandler<CreateOdooPaymentByPaymentSmsIdCommand, Result>
    {
        readonly IOdooIntegrationSerivce _odooIntegrationSerivce;
        readonly PaymentDbContext _paymentDbContext;

        public CreateOdooPaymentByPaymentSmsIdCommandHandler(IOdooIntegrationSerivce odooIntegrationSerivce, PaymentDbContext paymentDbContext)
        {
            _odooIntegrationSerivce = odooIntegrationSerivce;
            _paymentDbContext = paymentDbContext;
        }

        public async Task<Result> Handle(CreateOdooPaymentByPaymentSmsIdCommand request, CancellationToken cancellationToken)
        {
            //get payment by payment sms id
            Maybe<PaymentSm> mbPaymentSms = await _paymentDbContext.PaymentSms.FirstOrDefaultAsync(x => x.Id == request.PaymentSmsId) ?? default!;
            if (mbPaymentSms.HasNoValue) return Result.Failure(new Error("CreateOdooPaymentByPaymentSmsIdCommand.NotFound", "Payment sms not found"));

            //create odoo payment
            Result result = await _odooIntegrationSerivce.CreateOdooPayment(mbPaymentSms);

            if (result.IsSuccess)
            {
                _paymentDbContext.PaymentSms.Update(mbPaymentSms.Value);
                await _paymentDbContext.SaveChangesAsync();
            }
            return result;
        }
    }
}
