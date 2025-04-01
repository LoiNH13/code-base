namespace Sale.Contract.TimeFrameProductPrices;

/// <summary>
/// Represents a request to update the price of a product within a specific time frame.
/// </summary>
public class UpdateTimeFrameProductPriceRequest
{
    /// <summary>
    /// The new price of the product within the specified time frame.
    /// </summary>
    public double Price { get; set; }
}