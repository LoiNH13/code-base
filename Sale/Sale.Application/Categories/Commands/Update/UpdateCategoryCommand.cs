using Domain.Core.Primitives.Result;

namespace Sale.Application.Categories.Commands.Update;

public sealed class UpdateCategoryCommand : ICommand<Result>
{
    public UpdateCategoryCommand(Guid id, string name, int? odooRef, int? weight, bool isShowSalePlan, bool isShowMonthlyReport)
    {
        Id = id;
        Name = name;
        OdooRef = odooRef;
        Weight = weight ?? 0;
        IsShowSalePlan = isShowSalePlan;
        IsShowMonthlyReport = isShowMonthlyReport;
    }

    public Guid Id { get; }

    public string Name { get; }

    public int? OdooRef { get; }

    public int Weight { get; }

    public bool IsShowSalePlan { get; }

    public bool IsShowMonthlyReport { get; }
}
