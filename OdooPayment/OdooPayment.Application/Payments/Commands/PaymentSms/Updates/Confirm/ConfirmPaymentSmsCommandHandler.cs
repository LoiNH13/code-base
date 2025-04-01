using Application.Core.Abstractions.Messaging;
using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using Microsoft.EntityFrameworkCore;
using Odoo.Domain.Entities;
using Odoo.Domain.Repositories;
using Payment.Persistence.Models;

namespace OdooPayment.Application.Payments.Commands.PaymentSms.Updates.Confirm
{
    internal sealed class ConfirmPaymentSmsCommandHandler : ICommandHandler<ConfirmPaymentSmsCommand, Result>
    {
        readonly PaymentDbContext _dbContext;
        readonly IOdooAccountMoveRepository _odooAccountMoveRepository;

        public ConfirmPaymentSmsCommandHandler(PaymentDbContext dbContext, IOdooAccountMoveRepository odooAccountMoveRepository)
        {
            _dbContext = dbContext;
            _odooAccountMoveRepository = odooAccountMoveRepository;
        }

        public async Task<Result> Handle(ConfirmPaymentSmsCommand request, CancellationToken cancellationToken)
        {
            Maybe<PaymentSm> mbPaymentSms = await _dbContext.PaymentSms.FirstOrDefaultAsync(x => x.Id == request.PaymentSmsId) ?? default!;
            if (mbPaymentSms.HasNoValue)
                return Result.Failure(new Domain.Core.Primitives.Error("PaymentSms.NotFound", "PaymentSms not found"));

            string? accountMoveName = string.Empty;
            if (int.TryParse(mbPaymentSms.Value.InboundPaymentNumber, out int paymentNumber))
            {
                Maybe<AccountMove> mbAccountMove = await _odooAccountMoveRepository.GetByAccountPaymentId(paymentNumber);
                if (mbAccountMove.HasValue)
                {
                    accountMoveName = mbAccountMove.Value.Name;
                }

            }
            UpdatePayment(mbPaymentSms.Value, accountMoveName);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        private void UpdatePayment(PaymentSm paymentSms, string? accountMoveName)
        {
            paymentSms.InboundPaymentNumber = string.IsNullOrWhiteSpace(accountMoveName)
                ? paymentSms.InboundPaymentNumber
                : accountMoveName;
            paymentSms.InboundPaymentIsCreated = true;
            paymentSms.InboundPaymentIsConfirm = 1;
            paymentSms.InboundPaymentDate = DateTime.Now;
            _dbContext.PaymentSms.Update(paymentSms);
        }
    }
}
