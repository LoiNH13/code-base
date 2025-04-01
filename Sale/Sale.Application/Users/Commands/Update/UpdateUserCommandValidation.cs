using Application.Core.Extensions;
using FluentValidation;
using Sale.Application.Core.Errors;

namespace Sale.Application.Users.Commands.Update;

internal sealed class UpdateUserCommandValidation : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidation()
    {
        RuleFor(x => x.Id).NotEmpty().WithError(ValidationErrors.UpdateUser.UserIdIsRequired);
    }
}
