using Domain.Core.Primitives.Maybe;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sale.Application.Core.Authentication;
using Sale.Contract.Core;
using Sale.Domain.Entities.Users;
using Sale.Domain.Repositories;

namespace Sale.Application.Core.Behaviors;

internal sealed class ManageUsersBehavior<TRequest, TResponse>(
    IUserRepository userRepository,
    IUserIdentifierProvider userIdentifierProvider)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ManagedByFilter, IRequest<TResponse>
    where TResponse : class
{
    /// <summary>
    /// Add user to request
    /// case 1: default (not select user) base token and role. a) if admin role then null but in application logic will not filter else load all sub users to filter
    /// case 2: have select users but flag include Sub-user = false then just filter with select users
    /// case 3: have select users and flag include Sub-user = true then load all sub users foreach user and filter.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="next"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!request.ManagedByUserIds.Any())
        {
            Maybe<User> mbUser = await userRepository.GetByIdAsync(userIdentifierProvider.UserId, true);
            if (mbUser.HasNoValue) return default!;
            else if (userIdentifierProvider.Role != Domain.Enumerations.ERole.Admin)
            {
                userRepository.LoadAllChilds(mbUser);
                request.AddUser(mbUser);
            }
        }
        else
        {
            List<User> users = await userRepository.Queryable().Where(x => request.ManagedByUserIds!.Contains(x.Id))
                .ToListAsync(cancellationToken);
            if (request.IncludeSubordinateUsers)
            {
                users.ForEach(userRepository.LoadAllChilds);
            }

            request.AddUsers(users);
        }

        if (request.Users.Any() || userIdentifierProvider.Role == Domain.Enumerations.ERole.Admin)
        {
            return await next();
        }

        return default!;
    }
}