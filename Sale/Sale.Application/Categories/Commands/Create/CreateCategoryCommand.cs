using Domain.Core.Primitives.Result;

namespace Sale.Application.Categories.Commands.Create;

public sealed class CreateCategoryCommand : ICommand<Result>
{
    public CreateCategoryCommand(string name, int? odooRef, int? weight, bool isShowSalePlan, bool isShowMonthlyReport)
    {
        Name = name;
        OdooRef = odooRef;
        Weight = weight ?? 0;
        IsShowSalePlan = isShowSalePlan;
        IsShowMonthlyReport = isShowMonthlyReport;
    }

    public string Name { get; }

    public int? OdooRef { get; }

    public int Weight { get; set; }

    public bool IsShowSalePlan { get; }

    public bool IsShowMonthlyReport { get; }
}
