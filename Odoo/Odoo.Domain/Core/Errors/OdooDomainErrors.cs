using Domain.Core.Primitives;

namespace Odoo.Domain.Core.Errors;

public static class OdooDomainErrors
{
    public static class OrderCode
    {
        public static Error InvalidFormat => new Error("OrderCode", "Sale order code has an invalid format");

        public static Error LongerThanAllowed => new Error("OrderCode", "Sale order code cannot be longer than {0} characters").AddParams(ValueObjects.OrderCode.MaxLength);

        public static Error NullOrEmpty => new Error("OrderCode", "Sale order code cannot be null or empty");
    }

    public static class AccountMoves
    {
        public static Error NotFound => new Error("AccountMoves", "Account move not found");
    }

    public static class Districts
    {
        public static Error NotFound => new Error("Districts", "District not found");
    }

    public static class States
    {
        public static Error NotFound => new Error("States", "State not found");
    }

    public static class Customer
    {
        public static Error NotFound => new Error("Customer", "Customer not found");
    }
}
