using Domain.Core.Primitives.Maybe;
using Sale.Contract.TimeFrames;

namespace Sale.Application.TimeFrames.Queries.TimeFramesByRange;

public sealed class GetTimeFramesByRangeQuery : IQuery<Maybe<List<TimeFrameResponse>>>
{
    public GetTimeFramesByRangeQuery(int fromYear, int fromMonth, int toYear, int toMonth)
    {
        FromMY = new MonthYearDto { Month = fromMonth, Year = fromYear };
        ToMY = new MonthYearDto { Month = toMonth, Year = toYear };
    }

    public MonthYearDto FromMY { get; }

    public MonthYearDto ToMY { get; }
}
