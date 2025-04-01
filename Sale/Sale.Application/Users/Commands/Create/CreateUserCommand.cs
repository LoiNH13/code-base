using Domain.Core.Primitives.Maybe;
using Sale.Contract.Users;

namespace Sale.Application.Users.Commands.Create;

public sealed class CreateUserCommand : ICommand<Maybe<UserResponse>>
{
    public CreateUserCommand(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;
    }

    public string Name { get; }
    public string Email { get; }
    public string Password { get; }
}
