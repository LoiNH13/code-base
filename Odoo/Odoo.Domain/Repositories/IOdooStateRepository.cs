using Domain.Core.Primitives.Maybe;
using Odoo.Domain.Entities;

namespace Odoo.Domain.Repositories;

public interface IOdooStateRepository
{
    IQueryable<ResCountryState> Queryable(string searchText);

    Task<Maybe<ResCountryState>> GetByIdAsync(int id);

    Task<Maybe<ResCountryState>> GetByIdAndIncludeAllAsync(int id);
}
