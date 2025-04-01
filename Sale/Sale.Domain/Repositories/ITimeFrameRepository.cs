using Domain.Core.Primitives.Maybe;
using Sale.Domain.Entities;

namespace Sale.Domain.Repositories;

public interface ITimeFrameRepository
{
    IQueryable<TimeFrame> Queryable();

    void Insert(TimeFrame timeFrame);

    Task<Maybe<TimeFrame>> GetByIdAsync(Guid id);
}
