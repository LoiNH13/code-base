using Domain.Core.Primitives.Maybe;
using MOT.Contract.Core.VietmapLive;

namespace MOT.Application.Core.Abstractions.VietmapLive;

public interface IVietmapLiveService
{
    Task<Maybe<VerifyResponse>> PartnerVerify(VerifyRequest request);
}