using Domain.Core.Abstractions;
using Domain.Core.Primitives;
using Domain.Core.Utility;

namespace Sale.Domain.Entities;

public sealed class TimeFrame : Entity, IAuditableEntity
{
    public TimeFrame(int year, int month) : base(Guid.NewGuid())
    {
        //make Ensure.GreaterThan >= current year
        Ensure.GreaterThan(year, DateTime.UtcNow.Year - 1, "The year must be greater than or equal to the current year.", nameof(year));
        //LessThanOrEqual current year +1
        Ensure.LessThanOrEqual(year, DateTime.UtcNow.Year + 1, "The year must be less than or equal to the current year + 1.", nameof(year));
        Ensure.GreaterThan(month, 0, "The month must be greater than 0.", nameof(month));
        Ensure.LessThanOrEqual(month, 12, "The month must be less than or equal to 12.", nameof(month));

        Year = year;
        Month = month;
    }

    public int Year { get; init; }

    public int Month { get; init; }

    public DateTime CreatedOnUtc { get; }

    public DateTime? ModifiedOnUtc { get; }

    public string YearMonth => $"{Year}-{Month}";

    public int ConvertMonths => Year * 12 + Month;

    private TimeFrame()
    {
    }
}
