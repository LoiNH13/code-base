using Domain.Core.Primitives;
using Domain.Core.Primitives.Result;
using Newtonsoft.Json;
using Odoo.Domain.Core.Errors;
using System.Text.RegularExpressions;

namespace Odoo.Domain.ValueObjects
{
    public sealed class OrderCode : ValueObject
    {
        public const int MaxLength = 15;
        public const string ValidPattern = @"\b(?:SO|DN|HN|S0)(?:-\d{2}-\d{2}-\d{5}|\w{9})\b";
        public const string CorrectPattern = @"\b(?:SO|DN|HN|S0)-\d{2}-\d{2}-\d{5}\b";

        private static readonly Lazy<Regex> FormatRegex =
        new Lazy<Regex>(() => new Regex(ValidPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase));

        /// <summary>
        /// Initializes a new instance of the <see cref="Email"/> class.
        /// </summary>
        /// <param name="value">The email value.</param>
        [JsonConstructor]
        private OrderCode(string value) => Value = Transform(value);

        /// <summary>
        /// Gets the email value.
        /// </summary>
        public string Value { get; }

        public static implicit operator string(OrderCode saleOrderCode) => saleOrderCode.Value;

        /// <summary>
        /// Creates a new instance of the <see cref="Email"/> class based on the specified value.
        /// </summary>
        /// <param name="code">The code value.</param>
        /// <returns>The result of the email creation process containing the email or an error.</returns>
        public static Result<OrderCode> Create(string code) =>
            Result.Create(code, OdooDomainErrors.OrderCode.NullOrEmpty)
                .Ensure(e => !string.IsNullOrWhiteSpace(e), OdooDomainErrors.OrderCode.NullOrEmpty)
                .Ensure(e => e.Length <= MaxLength, OdooDomainErrors.OrderCode.LongerThanAllowed)
                .Ensure(e => FormatRegex.Value.IsMatch(e), OdooDomainErrors.OrderCode.InvalidFormat)
                .Map(e => new OrderCode(e));

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        /// <summary>
        /// Returns the string representation of the email.
        /// </summary>
        public override string ToString() => Value;

        public static string Transform(string value)
        {
            if (Regex.IsMatch(value, CorrectPattern))
            {
                // Thay thế prefix bằng SO và giữ phần còn lại
                return "SO" + value.Substring(2);
            }
            else
            {
                string numbers = value.Substring(2); // Bỏ 2 ký tự prefix

                // Lấy 2 số đầu và 2 số tiếp theo từ numbers
                string year = numbers.Substring(0, 2);
                string month = numbers.Substring(2, 2);
                string sequence = numbers.Substring(numbers.Length - 5).PadLeft(5, '0');

                return $"SO-{year}-{month}-{sequence}";
            }
        }
    }
}
