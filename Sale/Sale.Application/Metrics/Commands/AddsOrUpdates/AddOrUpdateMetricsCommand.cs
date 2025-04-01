using Domain.Core.Primitives.Result;
using Sale.Contract.Metrics;

namespace Sale.Application.Metrics.Commands.AddsOrUpdates;

public sealed class AddOrUpdateMetricsCommand : ICommand<Result>
{
    public AddOrUpdateMetricsCommand(Guid customerId, List<AddOrUpdateProductMetricsDto> metricProducts)
    {
        CustomerId = customerId;
        MetricProducts = metricProducts;
    }

    public Guid CustomerId { get; }

    public List<AddOrUpdateProductMetricsDto> MetricProducts { get; }

    public List<Guid> GetProductIds()
    {
        return MetricProducts.Select(x => x.ProductId).ToList();
    }

    public List<Guid> GetTimeFrameIds()
    {
        return MetricProducts.SelectMany(x => x.TimeFrameMetrics!.Select(metricDto => metricDto.TimeFrameId)).Distinct()
            .ToList();
    }
}