using FluentValidation;
using Sale.Application.Core.Errors;

namespace Sale.Application.ProductPrices.Commands.Create;

internal sealed class CreateProductPriceCommandValidation : AbstractValidator<CreateProductPriceCommand>
{
    public CreateProductPriceCommandValidation()
    {
        RuleFor(x => x.Price).GreaterThan(0).WithErrorCode(ValidationErrors.CreateProductPriceCommand.MustBeGreaterThanZero);
    }
}
