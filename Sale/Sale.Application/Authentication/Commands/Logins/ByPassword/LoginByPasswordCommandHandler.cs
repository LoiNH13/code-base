using Contract.Authentication;
using Domain.Core.Errors;
using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using Domain.Services;
using Domain.ValueObjects;
using Sale.Application.Core.Authentication;
using Sale.Domain.Entities.Users;
using Sale.Domain.Repositories;

namespace Sale.Application.Authentication.Commands.Logins.ByPassword;

internal sealed class LoginByPasswordCommandHandler(
    IUserRepository userRepository,
    IPasswordHashChecker passwordHasher,
    IJwtProvider jwtProvider)
    : ICommandHandler<LoginByPasswordCommand, Result<TokenResponse>>
{
    public async Task<Result<TokenResponse>> Handle(LoginByPasswordCommand request, CancellationToken cancellationToken)
    {
        Result<Email> email = Email.Create(request.Email);
        if (email.IsFailure) return Result.Failure<TokenResponse>(DomainErrors.Authentication.InvalidEmailOrPassword);

        Maybe<User> mbUser = await userRepository.GetUserByEmail(email.Value);
        if (mbUser.HasNoValue) return Result.Failure<TokenResponse>(DomainErrors.Authentication.InvalidEmailOrPassword);

        if (!mbUser.Value.VerifyPasswordHash(request.Password, passwordHasher))
            return Result.Failure<TokenResponse>(DomainErrors.Authentication.InvalidEmailOrPassword);

        Maybe<string> token = jwtProvider.Create(mbUser.Value.Id.ToString(), mbUser.Value.Name, mbUser.Value.Email,
            mbUser.Value.Role);
        return Result.Success(new TokenResponse(token.Value));
    }
}