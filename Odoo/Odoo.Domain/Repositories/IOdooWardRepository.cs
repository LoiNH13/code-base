using Domain.Core.Primitives.Maybe;
using Odoo.Domain.Entities;

namespace Odoo.Domain.Repositories;

public interface IOdooWardRepository
{
    IQueryable<ResWard> Queryable(int? districtId, string searchText);

    Task<Maybe<ResWard>> GetByIdAsync(int id);
}