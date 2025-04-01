using Contract.Common;
using Domain.Core.Abstractions;
using System.Linq.Expressions;

namespace Application.Core.Extensions;

public static class LinqExtensions
{
    public static IQueryable<TEntity> Paginate<TEntity>(this IQueryable<TEntity> query,
                                                        int pageNumber,
                                                        int pageSize,
                                                        out Paged paged) where TEntity : IAuditableEntity
    {
        paged = new Paged(pageNumber, pageSize, query.Count());
        return query.OrderByDescending(x => x.ModifiedOnUtc ?? x.CreatedOnUtc).Skip((pageNumber - 1) * pageSize).Take(pageSize);
    }

    public static IQueryable<T> PaginateNotEntity<T>(this IQueryable<T> query,
                                                     int pageNumber,
                                                     int pageSize,
                                                     out Paged paged)
    {
        paged = new Paged(pageNumber, pageSize, query.Count());
        return query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
    }

    public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, bool condition, Expression<Func<T, bool>> predicate)
    {
        if (condition)
        {
            return source.Where(predicate);
        }

        return source;
    }

    public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, bool> predicate)
    {
        if (condition)
        {
            return source.Where(predicate);
        }
        return source;
    }
}
