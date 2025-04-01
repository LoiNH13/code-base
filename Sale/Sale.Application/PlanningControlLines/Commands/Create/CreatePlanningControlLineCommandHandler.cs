using Application.Core.Abstractions.Data;
using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using Sale.Domain.Core.Errors;
using Sale.Domain.Entities;
using Sale.Domain.Entities.Planning;
using Sale.Domain.Repositories;

namespace Sale.Application.PlanningControlLines.Commands.Create;

internal sealed class CreatePlanningControlLineCommandHandler(
    IPlanningControlRepository planningControlRepository,
    IUnitOfWork unitOfWork,
    ITimeFrameRepository timeFrameRepository)
    : ICommandHandler<CreatePlanningControlLineCommand, Result>
{
    public async Task<Result> Handle(CreatePlanningControlLineCommand request, CancellationToken cancellationToken)
    {
        Maybe<PlanningControl> mbPlanningControl =
            await planningControlRepository.GetByIdAsync(request.PlanningControlId);
        if (mbPlanningControl.HasNoValue) return Result.Failure(SaleDomainErrors.PlanningControl.NotFound);

        Maybe<TimeFrame> mbTimeFrame = await timeFrameRepository.GetByIdAsync(request.TimeFrameId);
        if (mbTimeFrame.HasNoValue) return Result.Failure(SaleDomainErrors.TimeFrame.NotFound);

        Result result = mbPlanningControl.Value.AddLine(mbTimeFrame, request.IsOriginalBudget, request.IsTarget);
        if (result.IsSuccess) await unitOfWork.SaveChangesAsync(cancellationToken);
        return result;
    }
}