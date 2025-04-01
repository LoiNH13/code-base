using Application.Core.Abstractions.Messaging;
using Domain.Core.Primitives.Result;

namespace Sms.Application.ResMos.Commands.Create;

public sealed class CreateResMoCommand(string servicePhone, double pricePerMo, double freeMtPerMo) : ICommand<Result>
{
    public string ServicePhone { get; } = servicePhone;

    public double PricePerMo { get; } = pricePerMo;

    public double FreeMtPerMo { get; } = freeMtPerMo;
}