using Application.Core.Abstractions.Data;
using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using Sale.Domain.Core.Errors;
using Sale.Domain.Entities.Planning;
using Sale.Domain.Repositories;

namespace Sale.Application.PlanningControlLines.Commands.Delete;

internal sealed class DeletePlanningControlLineCommandHandler(
    IPlanningControlRepository planningControlRepository,
    IUnitOfWork unitOfWork,
    IPlanningControlLineRepository planningControlLineRepository)
    : ICommandHandler<DeletePlanningControlLineCommand, Result>
{
    public async Task<Result> Handle(DeletePlanningControlLineCommand request, CancellationToken cancellationToken)
    {
        Maybe<PlanningControl> mbPlanningControl =
            await planningControlRepository.GetByIdAsync(request.PlanningControlId);
        if (mbPlanningControl.HasNoValue) return Result.Failure(SaleDomainErrors.PlanningControl.NotFound);

        Result<PlanningControlLine> result = mbPlanningControl.Value.DeleteLine(request.PlanningControlLineId);
        if (result.IsSuccess)
        {
            planningControlLineRepository.Remove(result.Value);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return result;
    }
}