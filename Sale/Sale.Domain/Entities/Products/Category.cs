using Domain.Core.Abstractions;
using Domain.Core.Primitives;
using Domain.Core.Primitives.Result;

namespace Sale.Domain.Entities.Products;

public class Category : Entity, IAuditableEntity, ISoftDeletableEntity
{
    public Category(string name, int? odooRef, int weight, bool isShowSalePlan, bool isShowMonthlyReport)
    {
        Name = name;
        OdooRef = odooRef;
        Weight = weight;
        IsShowSalePlan = isShowSalePlan;
        IsShowMonthlyReport = isShowMonthlyReport;
    }

    public string Name { get; private set; } = string.Empty;

    public int? OdooRef { get; private set; }

    public int Weight { get; private set; }

    public bool IsShowSalePlan { get; private set; }

    public bool IsShowMonthlyReport { get; private set; }

    public DateTime CreatedOnUtc { get; }

    public DateTime? ModifiedOnUtc { get; }

    public DateTime? DeletedOnUtc { get; }

    public bool Deleted { get; }

    private Category()
    {
    }

    public Result Update(string name, int? odooRef, int weight, bool isShowSalePlan, bool isShowMonthlyReport)
    {
        Name = name;
        OdooRef = odooRef;
        Weight = weight;
        IsShowSalePlan = isShowSalePlan;
        IsShowMonthlyReport = isShowMonthlyReport;

        return Result.Success();
    }

    public void UpdateByJob(string name)
    {
        Name = name;
    }
}