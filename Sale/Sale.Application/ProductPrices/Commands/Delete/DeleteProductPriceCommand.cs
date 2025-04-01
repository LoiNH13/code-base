using Domain.Core.Primitives.Result;

namespace Sale.Application.ProductPrices.Commands.Delete;

public sealed class DeleteProductPriceCommand : ICommand<Result>
{
    public DeleteProductPriceCommand(Guid productId, Guid timeFrameProductPriceId)
    {
        ProductId = productId;
        TimeFrameProductPriceId = timeFrameProductPriceId;
    }

    public Guid ProductId { get; }

    public Guid TimeFrameProductPriceId { get; }
}
