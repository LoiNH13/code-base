using Domain.Core.Primitives;

namespace Domain.Core.Errors;

public static class DomainErrors
{
    public static class Authentication
    {
        public static Error InvalidEmailOrPassword => new Error(
            "Authentication.InvalidEmailOrPassword",
            "The specified email or password are incorrect.");
    }

    /// <summary>
    /// Contains the email errors.
    /// </summary>
    public static class Email
    {
        public static Error NullOrEmpty => new Error("Email.NullOrEmpty", "The email is required.");

        public static Error LongerThanAllowed => new Error("Email.LongerThanAllowed", "The email is longer than allowed.");

        public static Error InvalidFormat => new Error("Email.InvalidFormat", "The email format is invalid.");
    }

    /// <summary>
    /// Contains the password errors.
    /// </summary>
    public static class Password
    {
        public static Error NullOrEmpty => new Error("Password.NullOrEmpty", "The password is required.");

        public static Error TooShort => new Error("Password.TooShort", "The password is too short.");

        public static Error MissingUppercaseLetter => new Error(
            "Password.MissingUppercaseLetter",
            "The password requires at least one uppercase letter.");

        public static Error MissingLowercaseLetter => new Error(
            "Password.MissingLowercaseLetter",
            "The password requires at least one lowercase letter.");

        public static Error MissingDigit => new Error(
            "Password.MissingDigit",
            "The password requires at least one digit.");

        public static Error MissingNonAlphaNumeric => new Error(
            "Password.MissingNonAlphaNumeric",
            "The password requires at least one non-alphanumeric.");
    }

    /// <summary>
    /// Contains general errors.
    /// </summary>
    public static class General
    {
        public static Error UnProcessableRequest => new Error(
            "General.UnProcessableRequest",
            "The server could not process the request.");

        public static Error ServerError => new Error("General.ServerError", "The server encountered an unrecoverable error.");
    }
}
