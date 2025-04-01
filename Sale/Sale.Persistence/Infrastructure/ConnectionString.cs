namespace Sale.Persistence.Infrastructure;

/// <summary>
/// Represents a connection string.
/// </summary>
internal sealed class ConnectionString
{
    /// <summary>
    /// The connection strings key.
    /// </summary>
    public const string SettingsKey = "SaleDb";

    /// <summary>
    /// Initializes a new instance of the <see cref="ConnectionString"/> class.
    /// </summary>
    /// <param name="value">The connection string value.</param>
    public ConnectionString(string value) => Value = value;

    /// <summary>
    /// Gets the connection string value.
    /// </summary>
    private string Value { get; }

    public static implicit operator string(ConnectionString connectionString) => connectionString.Value;
}