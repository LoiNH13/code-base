using Domain.Core.Primitives;

namespace Domain.Core.Abstractions;

public interface IRepository<TEntity> where TEntity : Entity
{
    IQueryable<TEntity> Queryable();

    IQueryable<TEntity> QueryableSplitQuery();

    void Insert(TEntity entity);

    void InsertRange(IReadOnlyCollection<TEntity> entities);
}