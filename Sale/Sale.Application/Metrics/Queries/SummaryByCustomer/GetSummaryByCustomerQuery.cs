using Domain.Core.Primitives.Maybe;
using Sale.Contract.Metrics;
using Sale.Contract.TimeFrames;
using Sale.Domain.Enumerations;

namespace Sale.Application.Metrics.Queries.SummaryByCustomer;

public sealed class GetSummaryByCustomerQuery : IQuery<Maybe<List<SummaryByCustomerResponse>>>
{
    public GetSummaryByCustomerQuery(Guid customerId, MonthYearDto fromMY, MonthYearDto toMY, EMetricType metricType)
    {
        CustomerId = customerId;
        FromMY = fromMY;
        ToMY = toMY;
        MetricType = metricType;
    }

    public Guid CustomerId { get; }

    public MonthYearDto FromMY { get; }

    public MonthYearDto ToMY { get; }

    public EMetricType MetricType { get; }
}
