using Sale.Contract.Categories;
using Sale.Contract.TimeFrameProductPrices;
using Sale.Domain.Entities.Products;

namespace Sale.Contract.Products;

/// <summary>
/// Represents a product response object.
/// </summary>
public sealed class ProductResponse
{
    public Guid Id { get; set; }

    public Guid? CategoryId { get; set; }

    public string Name { get; set; }

    public int OdooRef { get; set; }

    public string? OdooCode { get; set; }

    public int Weight { get; set; }

    public double Price { get; set; }

    public bool Inactive { get; set; }

    public CategoryResponse? Category { get; set; }

    public List<ProductTimeFramePriceResponse>? ProductTimeFramePrices { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductResponse"/> class.
    /// </summary>
    /// <param name="product">The product entity to create the response from.</param>
    /// <param name="ignorePrices">If set to true, the product's time frame prices will not be included in the response.</param>
    public ProductResponse(Product product, bool ignorePrices = false)
    {
        Id = product.Id;
        Name = product.Name;
        OdooRef = product.OdooRef;
        OdooCode = product.OdooCode;
        Weight = product.Weight;
        Price = product.Price;
        Inactive = product.Inactive;
        if (product.Category is null) CategoryId = product.CategoryId;
        else Category = new CategoryResponse(product.Category);
        if (!ignorePrices)
        {
            ProductTimeFramePrices = product.ProductTimeFramePrices.Select(x => new ProductTimeFramePriceResponse(x)).ToList();
        }
    }
}
