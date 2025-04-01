using Application.Core.Extensions;
using FluentValidation;
using Sale.Application.Core.Errors;

namespace Sale.Application.Authentication.Commands.Logins.SSOVietmap;

public sealed class LoginVietmapSsoCommandValidation : AbstractValidator<LoginVietmapSsoCommand>
{
    public LoginVietmapSsoCommandValidation()
    {
        RuleFor(x => x.ClientId).NotEmpty().WithError(ValidationErrors.Login.ClientIdRequired);
        RuleFor(x => x.AccessToken).NotEmpty().WithError(ValidationErrors.Login.AccessToken);
    }
}
