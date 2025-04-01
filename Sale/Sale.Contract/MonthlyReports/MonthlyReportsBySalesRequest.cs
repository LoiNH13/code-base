namespace Sale.Contract.MonthlyReports;

public class MonthlyReportsBySalesRequest
{
    public int ConvertMonths { get; set; }

    public required List<Guid> ManagedByUserIds { get; set; }

    public bool? IncludeSubordinateUsers { get; set; }
}