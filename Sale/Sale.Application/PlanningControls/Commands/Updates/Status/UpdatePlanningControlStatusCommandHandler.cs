using Application.Core.Abstractions.Data;
using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using Sale.Domain.Core.Errors;
using Sale.Domain.Entities.Planning;
using Sale.Domain.Enumerations;
using Sale.Domain.Repositories;

namespace Sale.Application.PlanningControls.Commands.Updates.Status;

internal sealed class UpdatePlanningControlStatusCommandHandler(
    IPlanningControlRepository planningControlRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdatePlanningControlStatusCommand, Result>
{
    public async Task<Result> Handle(UpdatePlanningControlStatusCommand request, CancellationToken cancellationToken)
    {
        Maybe<PlanningControl> mbPlanningControl =
            await planningControlRepository.GetByIdAsync(request.PlanningControlId);
        if (mbPlanningControl.HasNoValue) return Result.Failure(SaleDomainErrors.PlanningControl.NotFound);

        Result result = mbPlanningControl.Value.UpdateStatus(PlanningControlStatus.FromValue(request.Status));
        if (result.IsSuccess) await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}