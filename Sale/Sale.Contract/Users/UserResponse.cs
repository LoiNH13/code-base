using Sale.Domain.Entities.Users;
using Sale.Domain.Enumerations;

namespace Sale.Contract.Users;

public class UserResponse
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserResponse"/> class.
    /// </summary>
    /// <param name="user">The user entity to create a response from.</param>
    public UserResponse(User user)
    {
        Id = user.Id;
        Name = user.Name;
        Email = user.Email.Value;
        ManagedByUserId = user.ManagedByUserId;
        Role = user.Role;
        BusinessType = user.BusinessType;
    }

    /// <summary>
    /// Gets or sets the unique identifier of the user.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the user.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the email of the user.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the user who manages this user.
    /// </summary>
    public Guid? ManagedByUserId { get; set; }


    /// <summary>
    /// Gets or sets the business type of the user.
    /// </summary>
    public EBusinessType BusinessType { get; set; }

    /// <summary>
    /// Gets or sets the role of the user.
    /// </summary>
    public ERole Role { get; set; }

    /// <summary>
    /// Gets the user who manages this user.
    /// </summary>
    public UserResponse? ManagedByUser { get; private set; }

    /// <summary>
    /// Gets the subordinate users of this user.
    /// </summary>
    public List<UserResponse>? SubordinateUsers { get; private set; }

    /// <summary>
    /// Creates a new <see cref="UserResponse"/> instance with subordinate users.
    /// </summary>
    /// <param name="user">The user entity to create a response from.</param>
    /// <returns>A new <see cref="UserResponse"/> instance with subordinate users.</returns>
    public static UserResponse CreateTree(User user)
    {
        UserResponse newUser = new(user);
        newUser.SubordinateUsers = user.SubordinateUsers.Select(x => CreateTree(x)).ToList();
        return newUser;
    }

    /// <summary>
    /// Creates a new <see cref="UserResponse"/> instance with the managing user.
    /// </summary>
    /// <param name="user">The user entity to create a response from.</param>
    /// <returns>A new <see cref="UserResponse"/> instance with the managing user.</returns>
    public static UserResponse Create(User user)
    {
        UserResponse newUser = new(user);
        newUser.ManagedByUser = user.ManagedByUser is null ? default : new(user.ManagedByUser);
        return newUser;
    }
}
