using Sale.Domain.Entities.Products;

namespace Sale.Contract.Categories;

/// <summary>
/// Represents a response model for a category.
/// </summary>
public class CategoryResponse
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CategoryResponse"/> class.
    /// </summary>
    /// <param name="category">The category entity to create the response from.</param>
    public CategoryResponse(Category category)
    {
        Id = category.Id;
        Name = category.Name;
        OdooRef = category.OdooRef;
        Weight = category.Weight;
        IsShowMonthlyReport = category.IsShowMonthlyReport;
        IsShowSalePlan = category.IsShowSalePlan;
    }

    /// <summary>
    /// Gets or sets the unique identifier of the category.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the category.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the reference to the corresponding record in the Odoo database.
    /// </summary>
    public int? OdooRef { get; set; }

    /// <summary>
    /// Gets or sets the weight of the category.
    /// </summary>
    public int Weight { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the category should be shown in the monthly report.
    /// </summary>
    public bool IsShowMonthlyReport { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the category should be shown in the sale plan.
    /// </summary>
    public bool IsShowSalePlan { get; set; }
}
