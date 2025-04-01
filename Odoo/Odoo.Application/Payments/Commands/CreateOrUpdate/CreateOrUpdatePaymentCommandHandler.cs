using Application.Core.Abstractions.Messaging;
using Domain.Core.Primitives.Result;
using Odoo.Contract.Services.Payments;

namespace Odoo.Application.Payments.Commands.CreateOrUpdate
{
    internal sealed class CreateOrUpdatePaymentCommandHandler : ICommandHandler<CreateOrUpdatePaymentCommand, Result<CreateOrUpdatePaymentResponse>>
    {
        public Task<Result<CreateOrUpdatePaymentResponse>> Handle(CreateOrUpdatePaymentCommand request, CancellationToken cancellationToken)
        {

            throw new NotImplementedException();

        }
    }
}
