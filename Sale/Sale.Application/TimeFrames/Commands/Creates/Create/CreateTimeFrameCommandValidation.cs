using Application.Core.Extensions;
using FluentValidation;
using Sale.Application.Core.Errors;

namespace Sale.Application.TimeFrames.Commands.Creates.Create;

internal sealed class CreateTimeFrameCommandValidation : AbstractValidator<CreateTimeFrameCommand>
{
    public CreateTimeFrameCommandValidation()
    {
        RuleFor(x => x.Year).Must(x => x >= DateTime.UtcNow.Year).WithError(ValidationErrors.CreateTimeFrameCommand.YearLowerThanCurrentYear);
        RuleFor(x => x.Month).Must(x => x is >= 1 and <= 12).WithError(ValidationErrors.CreateTimeFrameCommand.MonthMustInRange);
    }
}
