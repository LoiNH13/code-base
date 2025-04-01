using Domain.Core.Primitives;

namespace Sale.Application.Core.Errors;

internal static class ValidationErrors
{
    internal static class UpdatePlanningCustomerMappingOdooCommand
    {
        internal static Error CustomerIdIsRequired => new Error("UpdatePlanningCustomerMappingOdooCommand.CustomerIdIsRequired", "The customer identifier is required.");

        internal static Error OdooRefMustBeGreaterThanZero => new Error("UpdatePlanningCustomerMappingOdooCommand.OdooRefMustBeGreaterThanZero", "The odoo reference must be greater than zero.");
    }

    internal static class UpdateProductCommand
    {
        internal static Error PriceMustBePositive => new Error("UpdateProductCommand.PriceMustBePositive", "The price must be positive.");
    }

    internal static class CreateProductCommand
    {
        internal static Error PriceMustBePositive => new Error("CreateProductCommand.PriceMustBePositive", "The price must be positive.");
    }

    internal static class UpdateProductPriceCommand
    {
        internal static Error MustBeGreaterThanZero => new Error("UpdateProductPriceCommand.MustBeGreaterThanZero", "The price must be greater than zero.");
    }

    internal static class CreateProductPriceCommand
    {
        internal static Error MustBeGreaterThanZero => new Error("CreateProductPriceCommand.MustBeGreaterThanZero", "The price must be greater than zero.");
    }

    internal static class CreateTimeFrameCommand
    {
        internal static Error YearLowerThanCurrentYear => new Error("CreateTimeFrameCommand.YearMustThanCurrentYear", "The year must be greater than the current year.");

        internal static Error MonthMustInRange => new Error("CreateTimeFrameCommand.MonthMustInRange", "The month must be between 1 and 12.");
    }

    internal static class LoginByPasswordCommand
    {
        internal static Error EmailIsRequired => new Error("LoginByPasswordCommand.EmailIsRequired", "The email is required.");

        internal static Error PasswordIsRequired => new Error("LoginByPasswordCommand.PasswordIsRequired", "The password is required.");
    }

    internal static class UpdateMonthlyReport
    {
        internal static Error DateTimeMustUtcKind => new Error("UpdateMonthlyReport.DateTimeMustUtcKind", "The date time must be utc kind.");
    }

    internal static class CreateMonthlyReport
    {
        internal static Error DateTimeMustUtcKind => new Error("CreateMonthlyReport.DateTimeMustUtcKind", "The date time must be utc kind.");

        internal static Error DealerMustDefaultGroup => new Error("CreateMonthlyReport.DealerMustDefaultGroup", "The dealer must have default group.");
    }

    internal static class UpdateUser
    {
        internal static Error UserIdIsRequired => new Error("UpdateUser.UserIdIsRequired", "The user id is required.");
    }

    /// <summary>
    /// Contains the login errors.
    /// </summary>
    internal static class Login
    {
        internal static Error ClientIdRequired => new Error("Login.ClientIdRequired", "The client identifier is required.");

        internal static Error AccessToken => new Error("Login.AccessToken", "The access token is required.");

        internal static Error EmailIsRequired => new Error("Login.EmailIsRequired", "The email is required.");

        internal static Error PasswordIsRequired => new Error("Login.PasswordIsRequired", "The password is required.");
    }
}
