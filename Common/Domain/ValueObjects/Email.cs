using Domain.Core.Errors;
using Domain.Core.Primitives;
using Domain.Core.Primitives.Result;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects;

/// <summary>
/// Represents an email value object.
/// </summary>
public sealed class Email : ValueObject
{
    /// <summary>
    /// The maximum length of an email.
    /// </summary>
    public const int MaxLength = 256;

    // Regular expression pattern for email validation
    private const string EmailRegexPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

    // Lazily initialized regex for email format validation
    private static readonly Lazy<Regex> EmailFormatRegex =
        new Lazy<Regex>(() => new Regex(EmailRegexPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase));

    [JsonConstructor]
    /// <summary>
    /// Initializes a new instance of the <see cref="Email"/> class.
    /// </summary>
    /// <param name="value">The email value.</param>
    private Email(string value) => Value = value;

    /// <summary>
    /// Gets the email value.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Implicitly converts an Email object to a string.
    /// </summary>
    /// <param name="email">The Email object to convert.</param>
    public static implicit operator string(Email email) => email.Value;

    /// <summary>
    /// Creates a new instance of the <see cref="Email"/> class based on the specified value.
    /// </summary>
    /// <param name="email">The email value.</param>
    /// <returns>The result of the email creation process containing the email or an error.</returns>
    public static Result<Email> Create(string email) =>
        Result.Create(email, DomainErrors.Email.NullOrEmpty)
            .Ensure(e => !string.IsNullOrWhiteSpace(e), DomainErrors.Email.NullOrEmpty)
            .Ensure(e => e.Length <= MaxLength, DomainErrors.Email.LongerThanAllowed)
            .Ensure(e => EmailFormatRegex.Value.IsMatch(e), DomainErrors.Email.InvalidFormat)
            .Map(e => new Email(e.ToLower()));

    /// <inheritdoc />
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    /// <summary>
    /// Returns the string representation of the email.
    /// </summary>
    public override string ToString() => Value;
}
