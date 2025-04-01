using Domain.Core.Primitives.Maybe;
using Microsoft.EntityFrameworkCore;
using Odoo.Domain.Entities;
using Odoo.Domain.Repositories;
using Odoo.Persistence.Infrastructure;

namespace Odoo.Persistence.Repositories
{
    internal sealed class OdooAccountMoveRepository : IOdooAccountMoveRepository
    {
        readonly OdooDbContext _context;

        public OdooAccountMoveRepository(OdooDbContext context)
        {
            _context = context;
        }

        public async Task<Maybe<AccountMove>> GetByAccountPaymentId(int accountPaymentId) => await
            _context.AccountMoves.FirstOrDefaultAsync(x => x.AccountPayments.Any(x => x.Id == accountPaymentId)) ?? default!;

        public IQueryable<AccountMove> Queryable() => _context.AccountMoves.AsQueryable();
    }
}
