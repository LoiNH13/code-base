using Contract.Common;
using Domain.Core.Primitives.Maybe;
using Sale.Contract.TimeFrames;

namespace Sale.Application.TimeFrames.Queries.TimeFrames;

public sealed class TimeFramesQuery : IPagingQuery, IQuery<Maybe<PagedList<TimeFrameResponse>>>
{
    public TimeFramesQuery(int pageNumber, int pageSize, int? year)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        Year = year;
    }

    public int PageNumber { get; }

    public int PageSize { get; }

    public int? Year { get; }
}
