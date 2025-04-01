using Domain.Core.Primitives.Maybe;
using Odoo.Domain.Entities;

namespace Odoo.Domain.Repositories;

public interface IOdooDistrictRepository
{
    IQueryable<ResDistrict> Queryable(int? stateId, string searchText);

    Task<Maybe<ResDistrict>> GetByIdAsync(int id);
}