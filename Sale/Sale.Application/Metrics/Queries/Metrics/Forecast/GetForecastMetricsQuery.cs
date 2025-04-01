using Contract.Common;
using Domain.Core.Primitives.Maybe;
using Sale.Contract.Metrics;
using Sale.Contract.TimeFrames;

namespace Sale.Application.Metrics.Queries.Metrics.Forecast;

public sealed class GetForecastMetricsQuery : IPagingQuery, IQuery<Maybe<PagedList<GetMetricsResponse>>>
{
    public GetForecastMetricsQuery(int pageNumber,
                                   int pageSize,
                                   MonthYearDto fromMY,
                                   MonthYearDto toMY,
                                   Guid customerId,
                                   Guid categoryId,
                                   Guid? productId)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        FromMY = fromMY;
        ToMY = toMY;
        CustomerId = customerId;
        CategoryId = categoryId;
        ProductId = productId;
    }

    public int PageNumber { get; }

    public int PageSize { get; }

    public MonthYearDto FromMY { get; }

    public MonthYearDto ToMY { get; }

    public Guid CustomerId { get; }

    public Guid CategoryId { get; }

    public Guid? ProductId { get; }
}