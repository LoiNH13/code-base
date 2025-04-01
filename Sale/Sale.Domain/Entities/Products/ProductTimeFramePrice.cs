using Domain.Core.Abstractions;
using Domain.Core.Primitives;
using Domain.Core.Primitives.Result;

namespace Sale.Domain.Entities.Products;

public sealed class ProductTimeFramePrice : Entity, IAuditableEntity
{
    internal ProductTimeFramePrice(Product product, TimeFrame timeFrame, double price)
    {
        ProductId = product.Id;
        TimeFrameId = timeFrame.Id;
        Price = price;
    }

    public Guid ProductId { get; init; }

    public Guid TimeFrameId { get; init; }

    public double Price { get; private set; }

    public DateTime CreatedOnUtc { get; }

    public DateTime? ModifiedOnUtc { get; }

    public Product? Product { get; init; }

    public TimeFrame? TimeFrame { get; init; }

    private ProductTimeFramePrice() { }

    internal Result Update(double price)
    {
        Price = price;
        return Result.Success();
    }
}
