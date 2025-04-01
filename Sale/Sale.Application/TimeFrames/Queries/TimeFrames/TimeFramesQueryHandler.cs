using Application.Core.Extensions;
using Contract.Common;
using Domain.Core.Primitives.Maybe;
using Microsoft.EntityFrameworkCore;
using Sale.Contract.TimeFrames;
using Sale.Domain.Repositories;

namespace Sale.Application.TimeFrames.Queries.TimeFrames;

internal sealed class TimeFramesQueryHandler(ITimeFrameRepository timeFrameRepository)
    : IQueryHandler<TimeFramesQuery, Maybe<PagedList<TimeFrameResponse>>>
{
    public async Task<Maybe<PagedList<TimeFrameResponse>>> Handle(TimeFramesQuery request,
        CancellationToken cancellationToken)
    {
        var query = timeFrameRepository.Queryable()
            .WhereIf(request.Year.HasValue, x => x.Year == request.Year)
            .Paginate(request.PageNumber, request.PageSize, out Paged paged);

        if (paged.NotExists()) return new PagedList<TimeFrameResponse>(paged);

        var data = await query.Select(x => new TimeFrameResponse(x)).ToListAsync(cancellationToken);

        return new PagedList<TimeFrameResponse>(paged, data);
    }
}