using Application.Core.Abstractions.Data;
using Application.Core.Abstractions.Messaging;
using Domain.Core.Primitives.Result;
using Sms.Domain.Entities;
using Sms.Domain.Repositories;

namespace Sms.Application.ResMos.Commands.Create;

internal sealed class CreateResMoCommandHandler(IResMoRepository resMoRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateResMoCommand, Result>
{
    public async Task<Result> Handle(CreateResMoCommand request, CancellationToken cancellationToken)
    {
        ResMo resMo = new(request.ServicePhone, request.PricePerMo, request.FreeMtPerMo);

        resMoRepository.Insert(resMo);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}