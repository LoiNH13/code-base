using Sale.Contract.TimeFrames;
using Sale.Domain.Entities.Products;

namespace Sale.Contract.TimeFrameProductPrices;

/// <summary>
/// Represents a response model for product time frame prices.
/// </summary>
public class ProductTimeFramePriceResponse
{
    /// <summary>
    /// The unique identifier of the product time frame price.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The unique identifier of the time frame associated with the product price.
    /// This property can be null if the time frame is not assigned.
    /// </summary>
    public Guid? TimeFrameId { get; set; }

    /// <summary>
    /// The price value for the product within the specified time frame.
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// The detailed information about the time frame associated with the product price.
    /// This property can be null if the time frame is not assigned.
    /// </summary>
    public TimeFrameResponse? TimeFrame { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductTimeFramePriceResponse"/> class.
    /// </summary>
    /// <param name="productTimeFramePrice">The domain entity representing the product time frame price.</param>
    public ProductTimeFramePriceResponse(ProductTimeFramePrice productTimeFramePrice)
    {
        Id = productTimeFramePrice.Id;
        Price = productTimeFramePrice.Price;
        if (productTimeFramePrice.TimeFrame is null) TimeFrameId = productTimeFramePrice.TimeFrameId;
        else TimeFrame = new TimeFrameResponse(productTimeFramePrice.TimeFrame);
    }
}
