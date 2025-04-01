using Domain.Core.Primitives.Maybe;
using Microsoft.EntityFrameworkCore;
using Sale.Contract.TimeFrames;
using Sale.Domain.Repositories;

namespace Sale.Application.TimeFrames.Queries.TimeFramesByRange;

internal sealed class GetTimeFramesByRangeQueryHandler(ITimeFrameRepository timeFrameRepository)
    : IQueryHandler<GetTimeFramesByRangeQuery, Maybe<List<TimeFrameResponse>>>
{
    public async Task<Maybe<List<TimeFrameResponse>>> Handle(GetTimeFramesByRangeQuery request,
        CancellationToken cancellationToken)
    {
        return await timeFrameRepository.Queryable()
            .Where(tf =>
                request.FromMY.TotalMonths <= tf.Month + tf.Year * 12 &&
                request.ToMY.TotalMonths >= tf.Month + tf.Year * 12)
            .OrderBy(x => x.Month + x.Year * 12)
            .Select(x => new TimeFrameResponse(x))
            .ToListAsync(cancellationToken);
    }
}