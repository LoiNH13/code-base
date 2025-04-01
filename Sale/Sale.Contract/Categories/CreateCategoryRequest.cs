namespace Sale.Contract.Categories;

/// <summary>
/// Represents a request to create a new category.
/// </summary>
public class CreateCategoryRequest
{
    /// <summary>
    /// Gets or sets the name of the category. This property is required.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the reference ID in Odoo for this category. This property is optional.
    /// </summary>
    public int? OdooRef { get; set; }

    /// <summary>
    /// Gets or sets the weight of the category.
    /// </summary>
    public int Weight { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the category should be shown in the sale plan.
    /// </summary>
    public bool IsShowSalePlan { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the category should be shown in the monthly report.
    /// </summary>
    public bool IsShowMonthlyReport { get; set; }
}