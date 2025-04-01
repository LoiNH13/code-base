namespace Sale.Contract.MonthlyReports;

public class MonthlyReportsBySaleResponse
{
    public Guid UserId { get; set; }

    public int TotalCustomers { get; set; }

    public int TotalReports { get; set; }
}
