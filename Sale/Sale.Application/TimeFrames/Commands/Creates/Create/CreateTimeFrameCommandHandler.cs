using Application.Core.Abstractions.Data;
using Domain.Core.Primitives.Result;
using Sale.Domain.Entities;
using Sale.Domain.Repositories;

namespace Sale.Application.TimeFrames.Commands.Creates.Create;

internal sealed class CreateTimeFrameCommandHandler(ITimeFrameRepository timeFrameRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateTimeFrameCommand, Result>
{
    public async Task<Result> Handle(CreateTimeFrameCommand request, CancellationToken cancellationToken)
    {
        TimeFrame timeFrame = new TimeFrame(request.Year, request.Month);

        timeFrameRepository.Insert(timeFrame);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}