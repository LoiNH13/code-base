using Application.Core.Abstractions.Messaging;
using Domain.Core.Primitives.Result;

namespace OdooPayment.Application.Payments.Commands.PaymentSms.Updates.Confirm
{
    public sealed class ConfirmPaymentSmsCommand : ICommand<Result>
    {
        public int PaymentSmsId { get; }

        public ConfirmPaymentSmsCommand(int paymentSmsId) => PaymentSmsId = paymentSmsId;
    }
}
