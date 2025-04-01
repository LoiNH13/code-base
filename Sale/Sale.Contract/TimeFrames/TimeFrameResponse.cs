using Sale.Domain.Entities;

namespace Sale.Contract.TimeFrames;

/// <summary>
/// Represents a response model for a time frame entity.
/// </summary>
public sealed class TimeFrameResponse
{
    /// <summary>
    /// The unique identifier of the time frame.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The year of the time frame.
    /// </summary>
    public int Year { get; set; }

    /// <summary>
    /// The month of the time frame.
    /// </summary>
    public int Month { get; set; }

    /// <summary>
    /// Gets the year and month of the time frame in the format "YYYY-MM".
    /// </summary>
    public string YearMonth => $"{Year}-{Month}";

    /// <summary>
    /// Gets the quarter of the time frame.
    /// </summary>
    public int Quarter => (Month - 1) / 3 + 1;

    /// <summary>
    /// Gets the year and quarter of the time frame in the format "YYYY-Q{quarter}".
    /// </summary>
    public string YearQuarter => $"{Year}-Q{Quarter}";

    /// <summary>
    /// Initializes a new instance of the <see cref="TimeFrameResponse"/> class.
    /// </summary>
    /// <param name="timeFrame">The source time frame entity.</param>
    public TimeFrameResponse(TimeFrame timeFrame)
    {
        Id = timeFrame.Id;
        Year = timeFrame.Year;
        Month = timeFrame.Month;
    }
}
