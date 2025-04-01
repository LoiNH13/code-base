using Domain.Core.Primitives.Maybe;
using Odoo.Domain.Entities;

namespace Odoo.Domain.Repositories;

public interface IOdooCustomerRepository
{
    IQueryable<VietmapCustomerView> ViewQueryable();

    IQueryable<ResPartner> Queryable(string searchText);

    Task<Maybe<ResPartner>> GetCustomerById(int id);

    Task<Maybe<ResPartner>> GetCustomerByOdooRef(string odooRef);
}
