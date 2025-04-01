using Application.Core.Abstractions.Data;
using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using Sale.Application.Core.Authentication;
using Sale.Domain.Core.Errors;
using Sale.Domain.Entities.Planning;
using Sale.Domain.Entities.Users;
using Sale.Domain.Repositories;

namespace Sale.Application.PlanningApprovals.Commands.Updates.Status.EditRequest;

internal sealed class UpdateEditRequestStatusCommandHandler(
    IPlanningApprovalRepository planningApprovalRepository,
    IUnitOfWork unitOfWork,
    IUserRepository userRepository,
    IUserIdentifierProvider userIdentifierProvider)
    : ICommandHandler<UpdateEditRequestStatusCommand, Result>
{
    public async Task<Result> Handle(UpdateEditRequestStatusCommand request, CancellationToken cancellationToken)
    {
        Maybe<User> mbUser = await userRepository.GetByIdAsync(userIdentifierProvider.UserId, true);
        if (mbUser.HasNoValue) return Result.Failure(SaleDomainErrors.User.NotFound);

        Maybe<PlanningApproval> mbPlanningApproval =
            await planningApprovalRepository.GetByIdAsync(request.PlanningApprovalId);
        if (mbPlanningApproval.HasNoValue) return Result.Failure(SaleDomainErrors.PlanningApproval.NotFound);

        Result result = mbPlanningApproval.Value.EditRequest(mbUser.Value);
        if (result.IsSuccess) await unitOfWork.SaveChangesAsync(cancellationToken);

        return result;
    }
}