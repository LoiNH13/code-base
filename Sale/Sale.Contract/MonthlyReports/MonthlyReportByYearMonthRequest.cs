namespace Sale.Contract.MonthlyReports;

public sealed class MonthlyReportByYearMonthRequest
{
    public required List<Guid> CustomerIds { get; set; }
    public int Year { get; set; }
    public int? Month { get; set; }
}
