using Application.Core.Extensions;
using FluentValidation;
using Sale.Application.Core.Errors;

namespace Sale.Application.MonthlyReports.Commands.Create;

internal sealed class CreateMonthlyReportCommandValidator : AbstractValidator<CreateMonthlyReportCommand>
{
    public CreateMonthlyReportCommandValidator()
    {
        RuleFor(x => x.FromTimeOnUtc).Must(x => x.Kind == DateTimeKind.Utc)
            .WithError(ValidationErrors.CreateMonthlyReport.DateTimeMustUtcKind);

        RuleFor(x => x.ToTimeOnUtc).Must(x => (x?.Kind ?? DateTimeKind.Utc) == DateTimeKind.Utc)
            .WithError(ValidationErrors.CreateMonthlyReport.DateTimeMustUtcKind);

        RuleFor(x => new { x.DynamicValue, x.Items }).Must(x =>
        {
            if (x.Items is not null && x.DynamicValue.GetBusinessType() == Domain.Enumerations.EBusinessType.Dealer)
            {
                return !x.Items.Exists(y => y.Group != Domain.Enumerations.EMonthlyReportItem.Default);
            }
            return true;
        }).WithError(ValidationErrors.CreateMonthlyReport.DealerMustDefaultGroup);
    }
}
