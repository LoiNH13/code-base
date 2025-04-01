using Application.Core.Extensions;
using FluentValidation;
using Sale.Application.Core.Errors;

namespace Sale.Application.Products.Commands.Updates.Update;

internal sealed class UpdateProductCommandValidation : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidation()
    {
        RuleFor(x => x.Price).Must(x => x > 0).WithError(ValidationErrors.UpdateProductCommand.PriceMustBePositive);
    }
}
