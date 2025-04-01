using Application.Core.Abstractions.Messaging;
using Domain.Core.Primitives.Result;
using Odoo.Contract.Services.Payments;

namespace Odoo.Application.Payments.Commands.CreateOrUpdate
{
    public sealed class CreateOrUpdatePaymentCommand : ICommand<Result<CreateOrUpdatePaymentResponse>>
    {

    }
}
