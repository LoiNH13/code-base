using Domain.Core.Primitives.Maybe;
using Microsoft.EntityFrameworkCore;
using Odoo.Domain.Entities;
using Odoo.Domain.Repositories;
using Odoo.Persistence.Infrastructure;

namespace Odoo.Persistence.Repositories
{
    internal sealed class OdooAccountJournalRepository(OdooDbContext context) : IOdooAccountJournalRepository
    {
        public async Task<Maybe<AccountJournal>> GetByContainstName(string name) => await
            context.AccountJournals
            .FromSqlRaw(@"
            SELECT * FROM account_journal 
            WHERE name->>'en_US' LIKE {0} 
               OR name->>'vi_VN' LIKE {0}
            LIMIT 1",
            $"%{name}%").FirstOrDefaultAsync() ?? default!;

        public IQueryable<AccountJournal> Queryable() =>
            context.AccountJournals.AsQueryable();
    }
}
