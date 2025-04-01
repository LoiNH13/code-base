using Contract.Authentication;
using Domain.Core.Primitives.Result;

namespace Sale.Application.Authentication.Commands.Logins.SSOVietmap;

public sealed class LoginVietmapSsoCommand : ICommand<Result<TokenResponse>>
{
    public LoginVietmapSsoCommand(string clientId, string accessToken)
    {
        ClientId = clientId;
        AccessToken = accessToken;
    }

    public string ClientId { get; }
    public string AccessToken { get; }

}
