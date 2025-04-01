using Application.Core.Abstractions.Data;
using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using Sale.Domain.Core.Errors;
using Sale.Domain.Entities.Planning;
using Sale.Domain.Repositories;

namespace Sale.Application.PlanningControls.Commands.Updates.Update;

internal sealed class UpdatePlanningControlCommandHandler(
    IPlanningControlRepository planningControlRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdatePlanningControlCommand, Result>
{
    public async Task<Result> Handle(UpdatePlanningControlCommand request, CancellationToken cancellationToken)
    {
        Maybe<PlanningControl> mbPlanningControl =
            await planningControlRepository.GetByIdAsync(request.PlanningControlId);
        if (mbPlanningControl.HasNoValue) return Result.Failure(SaleDomainErrors.PlanningControl.NotFound);

        Result result = mbPlanningControl.Value.Update(request.Name);
        if (result.IsFailure) return result;

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return result;
    }
}