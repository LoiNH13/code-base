using Application.Core.Extensions;
using FluentValidation;
using Sale.Application.Core.Errors;

namespace Sale.Application.ProductPrices.Commands.Update;

internal sealed class UpdateProductPriceCommandValidation : AbstractValidator<UpdateProductPriceCommand>
{
    public UpdateProductPriceCommandValidation()
    {
        RuleFor(x => x.Price).GreaterThan(0).WithError(ValidationErrors.UpdateProductPriceCommand.MustBeGreaterThanZero);
    }
}
