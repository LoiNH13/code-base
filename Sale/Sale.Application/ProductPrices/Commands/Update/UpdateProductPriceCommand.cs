using Domain.Core.Primitives.Result;

namespace Sale.Application.ProductPrices.Commands.Update;

public sealed class UpdateProductPriceCommand : ICommand<Result>
{
    public UpdateProductPriceCommand(Guid productId, Guid timeFrameProductPriceId, double price)
    {
        ProductId = productId;
        TimeFrameProductPriceId = timeFrameProductPriceId;
        Price = price;
    }

    public Guid ProductId { get; }

    public Guid TimeFrameProductPriceId { get; }

    public double Price { get; }
}
