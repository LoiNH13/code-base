using Application.Core.Abstractions.Messaging;
using Application.Core.Extensions;
using Contract.Common;
using Domain.Core.Primitives.Maybe;
using Microsoft.EntityFrameworkCore;
using Sms.Contract.ResMos;
using Sms.Domain.Repositories;

namespace Sms.Application.ResMos.Queries.ResMos;

internal sealed class ResMosQueryHandler(IResMoRepository resMoRepository)
    : IQueryHandler<ResMosQuery, Maybe<PagedList<ResMoResponse>>>
{
    public async Task<Maybe<PagedList<ResMoResponse>>> Handle(ResMosQuery request, CancellationToken cancellationToken)
    {
        var query = resMoRepository.Queryable()
            .WhereIf(!string.IsNullOrWhiteSpace(request.ServicePhone), x => x.ServicePhone.Equals(request.ServicePhone))
            .Paginate(request.PageNumber, request.PageSize, out var paged);
        if (paged.NotExists()) return new PagedList<ResMoResponse>(paged);

        var resMos = await query.Select(x => new ResMoResponse(x.Id, x.ServicePhone, x.PricePerMo, x.FreeMtPerMo))
            .ToListAsync(cancellationToken);

        return new PagedList<ResMoResponse>(paged, resMos);
    }
}