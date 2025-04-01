namespace Sale.Contract.Products;

/// <summary>
/// Represents a request to create a new product.
/// </summary>
public class CreateProductRequest
{
    /// <summary>
    /// The unique identifier of the product category.
    /// </summary>
    public Guid CategoryId { get; set; }

    /// <summary>
    /// The name of the product. This field is required.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// The reference number of the product in the Odoo system.
    /// </summary>
    public int OdooRef { get; set; }

    /// <summary>
    /// The weight of the product, if applicable. This field is optional.
    /// </summary>
    public int? Weight { get; set; }

    /// <summary>
    /// The price of the product.
    /// </summary>
    public double Price { get; set; }
}
