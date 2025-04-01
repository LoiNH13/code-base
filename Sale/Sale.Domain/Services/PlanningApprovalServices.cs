using Domain.Core.Primitives.Result;
using Sale.Domain.Core.Errors;
using Sale.Domain.Entities.Customers;
using Sale.Domain.Entities.Users;
using Sale.Domain.Enumerations;
using Sale.Domain.Repositories;

namespace Sale.Domain.Services;

public sealed class PlanningApprovalServices(IUserRepository userRepository)
{
    /// <summary>
    /// Checks if a user can change the approval status of a customer.
    /// </summary>
    /// <param name="user">The user attempting to change the approval status.</param>
    /// <param name="customer">The customer whose approval status is being changed.</param>
    /// <param name="isMustLeader">Indicates whether the user must be a leader to change the approval status.</param>
    /// <returns>A <see cref="Result"/> indicating whether the user can change the approval status.</returns>
    public Result CanChangeApprovalStatusAsync(User user, Customer customer, bool isMustLeader)
    {
        if (user.Role == ERole.Admin) return Result.Success();

        if (user.Id == customer.ManagedByUserId && isMustLeader)
        {
            return Result.Failure(SaleDomainErrors.ERole.NotPermission);
        }
        else if (user.Id != customer.ManagedByUserId)
        {
            userRepository.LoadAllChilds(user);
            if (!user.GetUsersIncludeSubordinates().Exists(x => x.Id == customer.ManagedByUserId))
            {
                return Result.Failure(SaleDomainErrors.ERole.NotPermission);
            }
        }

        return Result.Success();
    }
}