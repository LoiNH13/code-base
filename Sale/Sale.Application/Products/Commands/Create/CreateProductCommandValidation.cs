using Application.Core.Extensions;
using FluentValidation;
using Sale.Application.Core.Errors;

namespace Sale.Application.Products.Commands.Create;

internal sealed class CreateProductCommandValidation : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidation()
    {
        RuleFor(x => x.Price).Must(x => x > 0).WithError(ValidationErrors.CreateProductCommand.PriceMustBePositive);
    }
}
