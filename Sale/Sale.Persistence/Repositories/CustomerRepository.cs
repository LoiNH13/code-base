using Application.Core.Abstractions.Data;
using Application.Core.Extensions;
using Domain.Core.Primitives.Maybe;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;
using Sale.Domain.Entities.Customers;
using Sale.Domain.Repositories;
using Sale.Persistence.Specifications;

namespace Sale.Persistence.Repositories;

internal sealed class CustomerRepository(IDbContext dbContext)
    : GenericRepository<Customer>(dbContext), ICustomerRepository
{
    public override async Task<Maybe<Customer>> GetByIdAsync(Guid id) =>
        await DbContext.Set<Customer>().Include(x => x.PlanNewCustomer).FirstOrDefaultAsync(x => x.Id == id) ??
        default!;

    public async Task<Maybe<Customer>> GetByOdooRefAsync(int odooRef) =>
        await DbContext.Set<Customer>().FirstOrDefaultAsync(x => x.OdooRef == odooRef) ?? default!;

    public async Task<Maybe<Customer>> GetPlanningByIdAsync(Guid id, List<Guid>? timeFrameIds) =>
        await DbContext.Set<Customer>().Where(x => x.Id == id)
            .Include(x => x.CustomerTimeFrames.Where(ctf => timeFrameIds == null || timeFrameIds.Contains(ctf.TimeFrameId)))
            .ThenInclude(x => x.Metrics).ThenInclude(x => x.ForeCast)
            .Include(x => x.CustomerTimeFrames).ThenInclude(x => x.Metrics).ThenInclude(x => x.OriginalBudget)
            .Include(x => x.CustomerTimeFrames).ThenInclude(x => x.Metrics).ThenInclude(x => x.Target)
            .AsSplitQuery()
            .FirstOrDefaultAsync() ?? default!;

    public async Task<bool> OdooRefExisted(int odooRef) => await
        DbContext.Set<Customer>().AnyAsync(x => x.OdooRef == odooRef);

    public async Task<int> PlanNewCustomerCount() =>
        await DbContext.Set<PlanNewCustomer>().IgnoreQueryFilters().CountAsync();

    public IQueryable<Customer> Queryable(string searchText) => Queryable()
        .WhereIf(!string.IsNullOrWhiteSpace(searchText), new CustomerWithSearchTextSpecification(searchText));

    public IQueryable<Customer> Queryable(string searchText, List<Guid> managedByUserIds, bool isAdmin = false)
        => Queryable(searchText).Where(new GenericFilterUserSpecification<Customer>(managedByUserIds, isAdmin));
}