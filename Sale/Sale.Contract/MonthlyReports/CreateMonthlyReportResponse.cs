namespace Sale.Contract.MonthlyReports;

/// <summary>
/// Represents the response from creating a monthly report.
/// </summary>
public sealed class CreateMonthlyReportResponse
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateMonthlyReportResponse"/> class.
    /// </summary>
    /// <param name="id">The unique identifier of the newly created monthly report.</param>
    public CreateMonthlyReportResponse(Guid id)
    {
        Id = id;
    }

    /// <summary>
    /// Gets the unique identifier of the newly created monthly report.
    /// </summary>
    public Guid Id { get; }
}
