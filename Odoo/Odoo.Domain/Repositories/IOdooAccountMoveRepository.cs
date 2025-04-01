using Domain.Core.Primitives.Maybe;
using Odoo.Domain.Entities;

namespace Odoo.Domain.Repositories
{
    public interface IOdooAccountMoveRepository
    {
        IQueryable<AccountMove> Queryable();

        Task<Maybe<AccountMove>> GetByAccountPaymentId(int accountPaymentId);
    }
}
