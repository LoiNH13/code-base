using Application.Core.Abstractions.Data;
using Domain.Core.Primitives.Result;
using Sale.Domain.Entities.Planning;
using Sale.Domain.Repositories;

namespace Sale.Application.PlanningControls.Commands.Create;

internal sealed class CreatePlanningControlCommandHandler(
    IPlanningControlRepository planningControlRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreatePlanningControlCommand, Result>
{
    public async Task<Result> Handle(CreatePlanningControlCommand request, CancellationToken cancellationToken)
    {
        PlanningControl planningControl = new PlanningControl(request.Name);

        planningControlRepository.Insert(planningControl);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}