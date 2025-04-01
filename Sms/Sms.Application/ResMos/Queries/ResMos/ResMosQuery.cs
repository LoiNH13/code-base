using Application.Core.Abstractions.Messaging;
using Contract.Common;
using Domain.Core.Primitives.Maybe;
using Sms.Contract.ResMos;

namespace Sms.Application.ResMos.Queries.ResMos;

public sealed class ResMosQuery(int pageNumber, int pageSize, string? servicePhone) : IPagingQuery, IQuery<Maybe<PagedList<ResMoResponse>>>
{
    public int PageNumber { get; } = pageNumber;

    public int PageSize { get; } = pageSize;

    public string ServicePhone { get; } = servicePhone ?? string.Empty;
}