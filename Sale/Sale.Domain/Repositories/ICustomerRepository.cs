using Domain.Core.Primitives.Maybe;
using Sale.Domain.Entities.Customers;

namespace Sale.Domain.Repositories;

public interface ICustomerRepository
{
    IQueryable<Customer> Queryable();

    IQueryable<Customer> Queryable(string searchText);

    IQueryable<Customer> Queryable(string searchText, List<Guid> managedByUserIds, bool isAdmin = false);

    IQueryable<Customer> QueryableSplitQuery();

    void Insert(Customer customer);

    Task<Maybe<Customer>> GetByIdAsync(Guid id);

    Task<Maybe<Customer>> GetPlanningByIdAsync(Guid id, List<Guid>? timeFrameIds);

    Task<Maybe<Customer>> GetByOdooRefAsync(int odooRef);

    Task<int> PlanNewCustomerCount();

    void Remove(Customer customer);

    Task<bool> OdooRefExisted(int odooRef);
}
