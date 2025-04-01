using Contract.Authentication;
using Domain.Core.Primitives.Result;

namespace Sale.Application.Authentication.Commands.Logins.ByPassword;

public sealed class LoginByPasswordCommand : ICommand<Result<TokenResponse>>
{
    public string Email { get; }
    public string Password { get; }

    public LoginByPasswordCommand(string email, string password)
    {
        Email = email;
        Password = password;
    }
}
