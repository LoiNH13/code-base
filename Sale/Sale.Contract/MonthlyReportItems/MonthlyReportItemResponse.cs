using Sale.Contract.Categories;
using Sale.Domain.Entities.MonthlyReports;
using Sale.Domain.Entities.Products;
using Sale.Domain.Enumerations;

namespace Sale.Contract.MonthlyReportItems;

/// <summary>
/// Represents a response model for a monthly report item.
/// </summary>
public class MonthlyReportItemResponse
{
    public Guid Id { get; set; }

    public Guid CategoryId { get; set; }

    public EMonthlyReportItem Group { get; set; }

    public double Quantity { get; set; }

    public double Revenue { get; set; }

    public string? Note { get; set; }

    public CategoryResponse? Category { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MonthlyReportItemResponse"/> class with basic properties from a monthly report item.
    /// </summary>
    /// <param name="monthlyReportItem">The source monthly report item.</param>
    public MonthlyReportItemResponse(MonthlyReportItem monthlyReportItem)
    {
        Id = monthlyReportItem.Id;
        CategoryId = monthlyReportItem.CategoryId;
        Quantity = monthlyReportItem.Quantity;
        Revenue = monthlyReportItem.Revenue;
        Note = monthlyReportItem.Note;
        Group = monthlyReportItem.Group;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MonthlyReportItemResponse"/> class with basic properties from a monthly report item and a category.
    /// </summary>
    /// <param name="monthlyReportItem">The source monthly report item.</param>
    /// <param name="category">The related category.</param>
    public MonthlyReportItemResponse(MonthlyReportItem monthlyReportItem, Category category)
    {
        Id = monthlyReportItem.Id;
        CategoryId = monthlyReportItem.CategoryId;
        Quantity = monthlyReportItem.Quantity;
        Revenue = monthlyReportItem.Revenue;
        Note = monthlyReportItem.Note;
        Group = monthlyReportItem.Group;
        Category = new CategoryResponse(category);
    }
}
