using Domain.Core.Primitives.Maybe;
using Sale.Contract.TimeFrames;

namespace Sale.Application.TimeFrames.Queries.TimeFrameById;

public sealed class TimeFrameByIdQuery : IQuery<Maybe<TimeFrameResponse>>
{
    public Guid TimeFrameId { get; }

    public TimeFrameByIdQuery(Guid timeFrameId) => TimeFrameId = timeFrameId;
}
