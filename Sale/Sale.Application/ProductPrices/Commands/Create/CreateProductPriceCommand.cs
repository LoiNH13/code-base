using Domain.Core.Primitives.Result;

namespace Sale.Application.ProductPrices.Commands.Create;

public sealed class CreateProductPriceCommand : ICommand<Result>
{
    public CreateProductPriceCommand(Guid productId, Guid timeFrameId, double price)
    {
        ProductId = productId;
        TimeFrameId = timeFrameId;
        Price = price;
    }

    public Guid ProductId { get; }

    public Guid TimeFrameId { get; }

    public double Price { get; }
}
