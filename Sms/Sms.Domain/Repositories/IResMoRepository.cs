using Domain.Core.Abstractions;
using Domain.Core.Primitives.Maybe;
using Sms.Domain.Entities;

namespace Sms.Domain.Repositories;

public interface IResMoRepository : IRepository<ResMo>
{
    Task<Maybe<ResMo>> GetByServiceNum(string serviceNum);
}