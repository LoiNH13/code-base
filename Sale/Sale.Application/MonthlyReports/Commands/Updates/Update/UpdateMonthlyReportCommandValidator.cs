using Application.Core.Extensions;
using FluentValidation;
using Sale.Application.Core.Errors;

namespace Sale.Application.MonthlyReports.Commands.Updates.Update;

internal sealed class UpdateMonthlyReportCommandValidator : AbstractValidator<UpdateMonthlyReportCommand>
{
    public UpdateMonthlyReportCommandValidator()
    {
        RuleFor(x => x.FromTimeOnUtc).Must(x => x.Kind == DateTimeKind.Utc).WithError(ValidationErrors.UpdateMonthlyReport.DateTimeMustUtcKind);

        RuleFor(x => x.ToTimeOnUtc).Must(x => (x?.Kind ?? DateTimeKind.Utc) == DateTimeKind.Utc).WithError(ValidationErrors.UpdateMonthlyReport.DateTimeMustUtcKind);
    }
}
