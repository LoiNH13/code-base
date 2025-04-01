using Application.Core.Extensions;
using FluentValidation;
using Sale.Application.Core.Errors;

namespace Sale.Application.Authentication.Commands.Logins.ByPassword;

internal sealed class LoginByPasswordCommandValidation : AbstractValidator<LoginByPasswordCommand>
{
    public LoginByPasswordCommandValidation()
    {
        RuleFor(x => x.Email).NotEmpty().WithError(ValidationErrors.LoginByPasswordCommand.EmailIsRequired);
        RuleFor(x => x.Password).NotEmpty().WithError(ValidationErrors.LoginByPasswordCommand.PasswordIsRequired);
    }
}
