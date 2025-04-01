using Application.Core.Abstractions.Data;
using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using Sale.Application.Core.Authentication;
using Sale.Domain.Core.Errors;
using Sale.Domain.Entities.Customers;
using Sale.Domain.Entities.Planning;
using Sale.Domain.Entities.Users;
using Sale.Domain.Repositories;
using Sale.Domain.Services;

namespace Sale.Application.PlanningApprovals.Commands.Updates.Status.WaitingForApprove;

internal sealed class UpdateWaitingForApproveStatusCommandHandler(
    ICustomerRepository customerRepository,
    IPlanningControlRepository planningControlRepository,
    IPlanningApprovalRepository planningApprovalRepository,
    IUnitOfWork unitOfWork,
    IUserRepository userRepository,
    IUserIdentifierProvider userIdentifierProvider)
    : ICommandHandler<UpdateWaitingForApproveStatusCommand, Result>
{
    public async Task<Result> Handle(UpdateWaitingForApproveStatusCommand request, CancellationToken cancellationToken)
    {
        Maybe<User> mbUser = await userRepository.GetByIdAsync(userIdentifierProvider.UserId, false);
        if (mbUser.HasNoValue) return Result.Failure(SaleDomainErrors.User.NotFound);

        PlanningApprovalServices planningApprovalServices = new PlanningApprovalServices(userRepository);

        Maybe<PlanningApproval> mbPlanningApproval =
            await planningApprovalRepository.GetByCustomerAndPlanning(request.CustomerId, request.PlanningControlId);
        if (mbPlanningApproval.HasNoValue)
        {
            Maybe<PlanningControl> mbPlanningControl =
                await planningControlRepository.GetByIdAsync(request.PlanningControlId);
            if (mbPlanningControl.HasNoValue) return Result.Failure(SaleDomainErrors.PlanningControl.NotFound);

            Maybe<Customer> mbCustomer = await customerRepository.GetPlanningByIdAsync(request.CustomerId,
                mbPlanningControl.Value.PlanningControlLines.Select(x => x.TimeFrameId).ToList());
            if (mbCustomer.HasNoValue) return Result.Failure(SaleDomainErrors.Customer.NotFound);

            Result<PlanningApproval> result =
                PlanningApproval.Create(mbPlanningControl, mbCustomer, mbUser, planningApprovalServices);
            if (result.IsSuccess)
            {
                planningApprovalRepository.Insert(result.Value);
                await unitOfWork.SaveChangesAsync(cancellationToken);
            }

            return result;
        }
        else
        {
            Maybe<Customer> mbCustomer = await customerRepository.GetPlanningByIdAsync(request.CustomerId,
                mbPlanningApproval.Value.PlanningControl.PlanningControlLines.Select(x => x.TimeFrameId).ToList());
            if (mbCustomer.HasNoValue) return Result.Failure(SaleDomainErrors.Customer.NotFound);

            Result result = mbPlanningApproval.Value.WaitingForApproval(mbCustomer, mbUser, planningApprovalServices);
            if (result.IsSuccess) await unitOfWork.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}