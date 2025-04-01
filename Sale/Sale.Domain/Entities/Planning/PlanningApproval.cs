using Domain.Core.Abstractions;
using Domain.Core.Primitives;
using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using Sale.Domain.Core.Errors;
using Sale.Domain.Entities.Customers;
using Sale.Domain.Entities.Metrics;
using Sale.Domain.Entities.Users;
using Sale.Domain.Enumerations;
using Sale.Domain.Services;

namespace Sale.Domain.Entities.Planning;

public sealed class PlanningApproval : AggregateRoot, IAuditableEntity, ISoftDeletableEntity
{
    internal PlanningApproval(PlanningControl planningControl, Customer customer, User changeStatusByUserId)
    {
        PlanningControlId = planningControl.Id;
        CustomerId = customer.Id;
        StatusByUserId = changeStatusByUserId.Id;
        CustomerManagedBy = customer.ManagedByUserId ?? changeStatusByUserId.Id;
        Status = PlanningApprovalStatus.WaitingForApproval;
    }

    public Guid PlanningControlId { get; init; }

    public Guid CustomerId { get; init; }

    public Guid CustomerManagedBy { get; private set; }

    public PlanningApprovalStatus Status { get; private set; } = PlanningApprovalStatus.WaitingForApproval;

    public double TotalTargetAmount { get; private set; }

    public double TotalOriginalBudgetAmount { get; private set; }

    public Guid StatusByUserId { get; private set; }

    public DateTime CreatedOnUtc { get; }

    public DateTime? ModifiedOnUtc { get; }

    public PlanningControl PlanningControl { get; } = default!;

    public DateTime? DeletedOnUtc { get; }

    public bool Deleted { get; }

    private PlanningApproval()
    {
    }

    private void UpdateAmount(PlanningControl planningControl, Customer customer)
    {
        List<Guid> targetTimeFrameIds = planningControl.PlanningControlLines.Where(x => x.IsTarget).Select(x => x.TimeFrameId).ToList();
        List<Guid> originalTimeFrameIds = planningControl.PlanningControlLines.Where(x => x.IsOriginalBudget).Select(x => x.TimeFrameId).ToList();

        if (targetTimeFrameIds.Count > 0)
            TotalTargetAmount = CustomerServices.TotalPlannedAmountByTimeFrames(customer, targetTimeFrameIds, EMetricType.ForeCast);
        if (originalTimeFrameIds.Count > 0)
            TotalOriginalBudgetAmount = CustomerServices.TotalPlannedAmountByTimeFrames(customer, originalTimeFrameIds, EMetricType.ForeCast);
    }

    public static Result<PlanningApproval> Create(PlanningControl planningControl, Customer customer, User actionByUser, PlanningApprovalServices services)
    {
        if (planningControl.Status != PlanningControlStatus.InProgress) return Result.Failure<PlanningApproval>(SaleDomainErrors.PlanningControl.StatusInvalid);

        Result checkStatus = services.CanChangeApprovalStatusAsync(actionByUser, customer, false);
        if (checkStatus.IsFailure) return Result.Failure<PlanningApproval>(checkStatus.Error);


        Result<PlanningApproval> result = new PlanningApproval(planningControl, customer, actionByUser);
        result.Value.UpdateAmount(planningControl, customer);
        return result;
    }

    public Result WaitingForApproval(Customer customer, User actionByUser, PlanningApprovalServices services)
    {
        if (Status == PlanningApprovalStatus.Locked) return Result.Failure(SaleDomainErrors.PlanningApproval.AlreadyLocked);
        if (Status == PlanningApprovalStatus.Approved) return Result.Failure(SaleDomainErrors.PlanningApproval.AlreadyApproved);
        Result result = services.CanChangeApprovalStatusAsync(actionByUser, customer, false);
        if (result.IsFailure) return result;

        Status = PlanningApprovalStatus.WaitingForApproval;
        StatusByUserId = actionByUser.Id;
        CustomerManagedBy = customer.ManagedByUserId ?? actionByUser.Id;
        UpdateAmount(PlanningControl!, customer);

        return Result.Success();
    }

    public Result Approve(Customer customer, User actionByUser, PlanningApprovalServices services)
    {
        if (PlanningControl.Status != PlanningControlStatus.InProgress) return Result.Failure<PlanningApproval>(SaleDomainErrors.PlanningControl.StatusInvalid);
        if (Status == PlanningApprovalStatus.Locked) return Result.Failure(SaleDomainErrors.PlanningApproval.AlreadyLocked);

        Result result = services.CanChangeApprovalStatusAsync(actionByUser, customer, true);
        if (result.IsFailure) return result;

        Status = PlanningApprovalStatus.Approved;
        StatusByUserId = actionByUser.Id;
        CustomerManagedBy = customer.ManagedByUserId ?? actionByUser.Id;

        UpdateAmount(PlanningControl, customer);

        foreach (var item in PlanningControl.PlanningControlLines)
        {
            Maybe<CustomerTimeFrame> mbCustomerTimeFrame = customer.CustomerTimeFrames.Find(x => x.TimeFrameId == item.TimeFrameId) ?? default!;
            if (mbCustomerTimeFrame.HasValue)
            {
                if (item.IsTarget) mbCustomerTimeFrame.Value.Metrics.ForEach(x => x.CloneToTarget());
                if (item.IsOriginalBudget) mbCustomerTimeFrame.Value.Metrics.ForEach(x => x.CloneToOriginalBudget());
            }
        }
        return Result.Success();
    }

    public Result EditRequest(User actionByUser)
    {
        if (PlanningControl.Status != PlanningControlStatus.InProgress) return Result.Failure<PlanningApproval>(SaleDomainErrors.PlanningControl.StatusInvalid);
        if (Status == PlanningApprovalStatus.Locked) return Result.Failure(SaleDomainErrors.PlanningApproval.AlreadyLocked);

        Status = PlanningApprovalStatus.EditRequest;
        StatusByUserId = actionByUser.Id;

        return Result.Success();
    }

    public Result LockStatus(Guid userId)
    {
        Status = PlanningApprovalStatus.Locked;
        StatusByUserId = userId;

        return Result.Success();
    }

    public Result UnlockStatus(User actionByUser)
    {
        if (PlanningControl.Status != PlanningControlStatus.InProgress) return Result.Failure<PlanningApproval>(SaleDomainErrors.PlanningControl.StatusInvalid);
        if (actionByUser.Role != ERole.Admin) return Result.Failure(SaleDomainErrors.User.MustAdmin);

        Status = PlanningApprovalStatus.EditRequest;
        StatusByUserId = actionByUser.Id;

        return Result.Success();
    }
}