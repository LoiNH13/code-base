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

namespace Sale.Application.PlanningApprovals.Commands.Updates.Status.Approved;

internal sealed class UpdateApprovedStatusCommandHandler(
    IPlanningApprovalRepository planningApprovalRepository,
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork,
    IUserRepository userRepository,
    IUserIdentifierProvider userIdentifierProvider)
    : ICommandHandler<UpdateApprovedStatusCommand, Result>
{
    public async Task<Result> Handle(UpdateApprovedStatusCommand request, CancellationToken cancellationToken)
    {
        Maybe<User> mbUser = await userRepository.GetByIdAsync(userIdentifierProvider.UserId, false);
        if (mbUser.HasNoValue) return Result.Failure(SaleDomainErrors.User.NotFound);

        PlanningApprovalServices planningApprovalServices = new PlanningApprovalServices(userRepository);

        Maybe<PlanningApproval> mbPlanningApproval =
            await planningApprovalRepository.GetByIdAsync(request.PlanningApprovalId);
        if (mbPlanningApproval.HasNoValue) return Result.Failure(SaleDomainErrors.PlanningApproval.NotFound);

        Maybe<Customer> mbCustomer = await customerRepository.GetPlanningByIdAsync(mbPlanningApproval.Value.CustomerId,
            mbPlanningApproval.Value
                .PlanningControl
                .PlanningControlLines
                .Select(x => x.TimeFrameId)
                .ToList());
        if (mbCustomer.HasNoValue) return Result.Failure(SaleDomainErrors.Customer.NotFound);

        Result result = mbPlanningApproval.Value.Approve(mbCustomer, mbUser, planningApprovalServices);
        if (result.IsSuccess) await unitOfWork.SaveChangesAsync(cancellationToken);

        return result;
    }
}