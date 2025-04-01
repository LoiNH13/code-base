using Domain.Core.Abstractions;
using Domain.Core.Primitives;
using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using Sale.Domain.Entities.Customers;
using Sale.Domain.Entities.Products;

namespace Sale.Domain.Entities.Metrics;

public sealed class CustomerTimeFrame : Entity, IAuditableEntity
{
    public CustomerTimeFrame(Customer customer, TimeFrame timeFrame) //: base(Guid.NewGuid())
    {
        CustomerId = customer.Id;
        TimeFrameId = timeFrame.Id;
        TimeFrame = timeFrame;
    }

    public Guid CustomerId { get; init; }

    public Guid TimeFrameId { get; init; }

    public DateTime CreatedOnUtc { get; }

    public DateTime? ModifiedOnUtc { get; }

    public TimeFrame? TimeFrame { get; init; }

    public List<Metric> Metrics { get; private set; } = new List<Metric>();

    private CustomerTimeFrame() { }

    internal Result<Metric> FindMetric(Product product)
    {
        Maybe<Metric> metric = Metrics.Find(x => x.ProductId == product.Id) ?? default!;
        if (metric.HasNoValue)
        {
            metric = new Metric(this, product, 0, 0);
            Metrics.Add(metric);
        }
        return Result.Success<Metric>(metric);
    }

    internal void ClearAllForeCast()
    {
        foreach (Metric metric in Metrics)
        {
            metric.ClearForecast();
        }
    }
}