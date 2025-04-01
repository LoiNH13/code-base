namespace Sale.Contract.TimeFrames;

/// <summary>
/// Represents a request to create a new time frame.
/// </summary>
public class CreateTimeFrameRequest
{
    /// <summary>
    /// Gets or sets the month for the time frame.
    /// </summary>
    /// <value>
    /// The month should be a value between 1 and 12.
    /// </value>
    public int Month { get; set; }

    /// <summary>
    /// Gets or sets the year for the time frame.
    /// </summary>
    /// <value>
    /// The year should be a valid Gregorian calendar year.
    /// </value>
    public int Year { get; set; }
}
