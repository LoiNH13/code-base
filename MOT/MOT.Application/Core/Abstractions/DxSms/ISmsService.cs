using Domain.Core.Primitives.Maybe;
using MOT.Contract.Core.DxSms;

namespace MOT.Application.Core.Abstractions.DxSms;

public interface ISmsService
{
    Task<Maybe<ResMoResponse>> GetResMoByServicePhone(string servicePhone, CancellationToken cancellationToken = default!);
}