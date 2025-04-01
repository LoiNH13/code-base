using Domain.Core.Primitives.Maybe;
using Odoo.Domain.Entities;

namespace Odoo.Domain.Repositories
{
    public interface IOdooAccountJournalRepository
    {
        IQueryable<AccountJournal> Queryable();

        Task<Maybe<AccountJournal>> GetByContainstName(string name);
    }
}
