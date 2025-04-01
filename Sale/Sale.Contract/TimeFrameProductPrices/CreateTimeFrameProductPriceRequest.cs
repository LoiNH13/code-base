
namespace Sale.Contract.TimeFrameProductPrices;

/// <summary>
/// Represents a request to create a new time frame product price.
/// </summary>
public class CreateTimeFrameProductPriceRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the time frame.
    /// </summary>
    public Guid TimeFrameId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the product.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the price for the product within the specified time frame.
    /// </summary>
    public double Price { get; set; }
}