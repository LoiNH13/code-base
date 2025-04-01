using Application.Core.Abstractions.Common;
using Application.Core.Abstractions.Cryptography;
using Application.Core.Abstractions.Data;
using Application.Core.Authentication;
using Contract.Authentication;
using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using Domain.ValueObjects;
using Sale.Application.Core.Authentication;
using Sale.Domain.Entities.Users;
using Sale.Domain.Repositories;

namespace Sale.Application.Authentication.Commands.Logins.SSOVietmap;

internal sealed class LoginVietmapSsoCommandHandler(
    IUnitOfWork unitOfWork,
    IJwtProvider jwtProvider,
    IVietmapAuthenticationService vietmapAuthenticationService,
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IDateTime dateTime)
    : ICommandHandler<LoginVietmapSsoCommand, Result<TokenResponse>>
{
    public async Task<Result<TokenResponse>> Handle(LoginVietmapSsoCommand request, CancellationToken cancellationToken)
    {
        Result<Email> email = await vietmapAuthenticationService.CheckProfile(request.ClientId, request.AccessToken);
        if (email.IsFailure) return Result.Failure<TokenResponse>(email.Error);

        Maybe<User> mbUser = await userRepository.GetUserByEmail(email.Value);
        if (mbUser.HasNoValue)
        {
            Result<Password> password =
                Password.Create(email.Value.Value.Replace("@vietmap.vn", $"@Vietmap{dateTime.UtcNow.Year}"));
            if (password.IsFailure) return Result.Failure<TokenResponse>(password.Error);

            mbUser = new User(email.Value, email.Value, passwordHasher.HashPassword(password.Value));
            userRepository.Insert(mbUser);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        Maybe<string> token = jwtProvider.Create(mbUser.Value.Id.ToString(), mbUser.Value.Name, mbUser.Value.Email,
            mbUser.Value.Role);
        return Result.Success(new TokenResponse(token.Value));
    }
}