using Application.Core.Abstractions.Data;
using Persistence.Repositories;
using Sms.Domain.Entities;
using Sms.Domain.Repositories;

namespace Sms.Persistence.Repositories;

internal sealed class MoMessageRepository(IDbContext dbContext)
    : GenericRepository<MoMessage>(dbContext), IMoMessageRepository;