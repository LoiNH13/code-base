using Domain.Core.Abstractions;
using Domain.Core.Primitives;
using Domain.Core.Primitives.Result;
using Sale.Domain.Entities.Products;

namespace Sale.Domain.Entities.Metrics;

public sealed class Metric : Entity, IAuditableEntity, ISoftDeletableEntity
{
    public Metric(CustomerTimeFrame customerTimeFrame, Product product, double orderNumber, double returnNumber) //: base(Guid.NewGuid())
    {
        CustomerTimeFrameId = customerTimeFrame.Id;
        ProductId = product.Id;
        OrderNumber = orderNumber;
        ReturnNumber = returnNumber;
        Product = product;
    }

    public Guid CustomerTimeFrameId { get; init; }

    public Guid ProductId { get; init; }

    public double OrderNumber { get; private set; }

    public double ReturnNumber { get; private set; }

    public List<int> OrderIds { get; private set; } = new();

    public List<int> ReturnIds { get; private set; } = new();

    public DateTime CreatedOnUtc { get; }

    public DateTime? ModifiedOnUtc { get; }

    public DateTime? DeletedOnUtc { get; }

    public bool Deleted { get; }

    public Product? Product { get; init; }

    public ForeCast? ForeCast { get; private set; }

    public Target? Target { get; private set; }

    public OriginalBudget? OriginalBudget { get; private set; }

    private Metric()
    {
    }

    internal Result AddOrUpdateForeCast(double price, double lastStockNumber, double wholeSalesNumber, double retailSalesNumber, double stockNumber)
    {
        if (ForeCast is null)
            ForeCast = new ForeCast(this, price, lastStockNumber, wholeSalesNumber, retailSalesNumber, stockNumber);
        else
            ForeCast.Update(price, lastStockNumber, wholeSalesNumber, retailSalesNumber, stockNumber);

        return Result.Success();
    }

    internal void UpdateOdooNumber(double orderNumber, double returnNumber, List<int> orderIds, List<int> returnIds)
    {
        OrderNumber = orderNumber;
        ReturnNumber = returnNumber;
        ReturnIds = returnIds;
        OrderIds = orderIds;
    }

    internal void CloneToTarget()
    {
        if (Target is not null)
            Target.Update(ForeCast!);
        else
            Target = new Target(ForeCast!);
    }

    internal void CloneToOriginalBudget()
    {
        if (OriginalBudget is not null)
            OriginalBudget.Update(ForeCast!);
        else
            OriginalBudget = new OriginalBudget(ForeCast!);
    }

    internal void ReCalculateForecast() => ForeCast!.ReCalculateStock(ReturnNumber);

    internal void ClearForecast()
    {
        if (ForeCast is not null)
            ForeCast.Clear();
    }
}
