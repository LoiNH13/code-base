using Sale.Contract.MonthlyReportItems;

namespace Sale.Contract.MonthlyReports;

public class CreateManufacturerMonthlyReportRequest
{
    public DateTime FromTimeOnUtc { get; set; }

    public DateTime? ToTimeOnUtc { get; set; }

    public string? Note { get; set; }

    public int OdooCustomerId { get; set; }

    public int NewBuyInMonth { get; set; }

    public int NewBuyNextMonth { get; set; }

    public double ServiceInMonth { get; set; }

    public List<CreateMonthlyReportItemRequest>? Items { get; set; }
}