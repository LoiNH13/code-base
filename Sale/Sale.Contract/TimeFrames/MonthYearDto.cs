using System.ComponentModel.DataAnnotations;

namespace Sale.Contract.TimeFrames;

/// <summary>
/// Represents a specific month and year.
/// </summary>
public class MonthYearDto : IComparable<MonthYearDto>
{
    /// <summary>
    /// Gets or sets the year. The value must be between 1 and 9999.
    /// </summary>
    [Range(1, 9999)]
    public int Year { get; set; }

    /// <summary>
    /// Gets or sets the month. The value must be between 1 and 12.
    /// </summary>
    [Range(1, 12)]
    public int Month { get; set; }

    /// <summary>
    /// Gets the total number of months since January 1, 0001.
    /// </summary>
    public int TotalMonths => Year * 12 + Month;

    public int CompareTo(MonthYearDto? other)
    {
        if (other is null) return 1;
        if (ReferenceEquals(this, other)) return 0;
        if (GetType() != other.GetType()) return -1;

        if (Year != other.Year) return Year.CompareTo(other.Year);
        return Month.CompareTo(other.Month);
    }

    public override bool Equals(object? obj)
    {
        if (obj is MonthYearDto other)
        {
            return Year == other.Year && Month == other.Month;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Year, Month);
    }

    public static bool operator ==(MonthYearDto? left, MonthYearDto? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(MonthYearDto? left, MonthYearDto? right)
    {
        return !(left == right);
    }

    public static bool operator <(MonthYearDto left, MonthYearDto right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator <=(MonthYearDto left, MonthYearDto right)
    {
        return left.CompareTo(right) <= 0;
    }

    public static bool operator >(MonthYearDto left, MonthYearDto right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator >=(MonthYearDto left, MonthYearDto right)
    {
        return left.CompareTo(right) >= 0;
    }
}