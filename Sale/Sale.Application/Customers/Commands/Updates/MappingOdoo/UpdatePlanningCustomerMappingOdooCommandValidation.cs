using Application.Core.Extensions;
using FluentValidation;
using Sale.Application.Core.Errors;

namespace Sale.Application.Customers.Commands.Updates.MappingOdoo;

internal sealed class UpdatePlanningCustomerMappingOdooCommandValidation : AbstractValidator<UpdatePlanningCustomerMappingOdooCommand>
{
    public UpdatePlanningCustomerMappingOdooCommandValidation()
    {
        RuleFor(x => x.CustomerId).NotEmpty().WithError(ValidationErrors.UpdatePlanningCustomerMappingOdooCommand.CustomerIdIsRequired);
        RuleFor(x => x.OdooRef).Must(x => x > 0).WithError(ValidationErrors.UpdatePlanningCustomerMappingOdooCommand.OdooRefMustBeGreaterThanZero);
    }
}
