using Application.Core.Abstractions.Messaging;
using Domain.Core.Primitives.Result;

namespace OdooPayment.Application.Payments.Commands.Odoos.Create
{
    public sealed class CreateOdooPaymentByPaymentSmsIdCommand : ICommand<Result>
    {
        public int PaymentSmsId { get; }

        public CreateOdooPaymentByPaymentSmsIdCommand(int paymentSmsId) => PaymentSmsId = paymentSmsId;
    }
}
