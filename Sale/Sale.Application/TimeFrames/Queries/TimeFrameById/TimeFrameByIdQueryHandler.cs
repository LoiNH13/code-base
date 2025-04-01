using Domain.Core.Primitives.Maybe;
using Sale.Contract.TimeFrames;
using Sale.Domain.Repositories;

namespace Sale.Application.TimeFrames.Queries.TimeFrameById;

internal sealed class TimeFrameByIdQueryHandler(ITimeFrameRepository timeFrameRepository)
    : IQueryHandler<TimeFrameByIdQuery, Maybe<TimeFrameResponse>>
{
    public async Task<Maybe<TimeFrameResponse>>
        Handle(TimeFrameByIdQuery request, CancellationToken cancellationToken)
    {
        return await timeFrameRepository.GetByIdAsync(request.TimeFrameId)
            .Match(x => new TimeFrameResponse(x), () => default!);
    }
}