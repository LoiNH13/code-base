using Application.Core.Abstractions.Data;
using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using Sale.Application.Core.Authentication;
using Sale.Domain.Core.Errors;
using Sale.Domain.Entities.Planning;
using Sale.Domain.Entities.Users;
using Sale.Domain.Repositories;

namespace Sale.Application.PlanningApprovals.Commands.Updates.Status.Unlock;

internal sealed class UpdateUnlockStatusCommandHandler(
    IPlanningApprovalRepository planningApprovalRepository,
    IUnitOfWork unitOfWork,
    IUserRepository userRepository,
    IUserIdentifierProvider userIdentifierProvider)
    : ICommandHandler<UpdateUnlockStatusCommand, Result>
{
    public async Task<Result> Handle(UpdateUnlockStatusCommand request, CancellationToken cancellationToken)
    {
        Maybe<User> mbUser = await userRepository.GetByIdAsync(userIdentifierProvider.UserId, false);
        if (mbUser.HasNoValue) return Result.Failure(SaleDomainErrors.User.NotFound);

        Maybe<PlanningApproval> mbPlanningApproval =
            await planningApprovalRepository.GetByIdAsync(request.PlanningApprovalId);
        if (mbPlanningApproval.HasNoValue) return Result.Failure(SaleDomainErrors.PlanningApproval.NotFound);

        Result result = mbPlanningApproval.Value.UnlockStatus(mbUser.Value);
        if (result.IsSuccess) await unitOfWork.SaveChangesAsync(cancellationToken);

        return result;
    }
}