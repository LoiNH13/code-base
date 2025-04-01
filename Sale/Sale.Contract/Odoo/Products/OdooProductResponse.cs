using Odoo.Domain.Entities;

namespace Sale.Contract.Odoo.Products;

/// <summary>
/// Represents a response from Odoo for a product.
/// </summary>
public class OdooProductResponse
{
    public int Id { get; set; }

    /// <summary>
    /// The internal reference code of the product in Odoo.
    /// </summary>
    public string DefaultCode { get; set; }

    /// <summary>
    /// The display name of the product in Odoo.
    /// </summary>
    public string DisplayName { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="OdooProductResponse"/> class.
    /// </summary>
    /// <param name="odooProduct">The Odoo product entity.</param>
    public OdooProductResponse(ProductProduct odooProduct)
    {
        Id = odooProduct.Id;
        DefaultCode = odooProduct.DefaultCode?.Trim() ?? string.Empty;
        DisplayName = odooProduct.DisplayName?.Trim() ?? string.Empty;
    }
}