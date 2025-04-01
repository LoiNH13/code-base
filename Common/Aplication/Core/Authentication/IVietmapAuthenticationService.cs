using Domain.Core.Primitives.Result;
using Domain.ValueObjects;

namespace Application.Core.Authentication;

public interface IVietmapAuthenticationService
{
    Task<Result<Email>> CheckProfile(string clientId, string accessToken);
}
