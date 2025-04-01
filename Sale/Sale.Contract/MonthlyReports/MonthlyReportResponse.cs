using Sale.Contract.Customers;
using Sale.Contract.MonthlyReportItems;
using Sale.Contract.Users;
using Sale.Domain.Entities.Customers;
using Sale.Domain.Entities.MonthlyReports;
using Sale.Domain.Entities.Users;
using Sale.Domain.Enumerations;
using Sale.Domain.ValueObjects;

namespace Sale.Contract.MonthlyReports;

/// <summary>
/// Represents a response model for a monthly report.
/// </summary>
public class MonthlyReportResponse
{
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public DateTime FromTimeOnUtc { get; set; }

    public DateTime ToTimeOnUtc { get; set; }

    public EBusinessType BusinessType { get; set; }

    public int DailyVisitors { get; set; }

    public int DailyPurchases { get; set; }

    public double OnlinePurchaseRate { get; set; }

    public string? Note { get; set; }

    public bool IsConfirmed { get; set; }

    public DealerMonthlyReport? DealerMonthlyReport { get; }

    public ManufacturerMonthlyReport? ManufacturerMonthlyReport { get; }

    public CustomerResponse? Customer { get; set; }

    public UserResponse? User { get; set; }

    public IReadOnlyList<MonthlyReportItemResponse>? Items { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MonthlyReportResponse"/> class with basic information from a monthly report.
    /// </summary>
    /// <param name="monthlyReport">The monthly report entity.</param>
    public MonthlyReportResponse(MonthlyReport monthlyReport)
    {
        Id = monthlyReport.Id;
        CustomerId = monthlyReport.CustomerId;
        FromTimeOnUtc = monthlyReport.FromTimeOnUtc;
        ToTimeOnUtc = monthlyReport.ToTimeOnUtc;
        DailyVisitors = monthlyReport.DailyVisitors;
        DailyPurchases = monthlyReport.DailyPurchases;
        OnlinePurchaseRate = monthlyReport.OnlinePurchaseRate;
        Note = monthlyReport.Note;
        IsConfirmed = monthlyReport.IsConfirmed;
        Items = monthlyReport.Items
            .Select(item => new MonthlyReportItemResponse(item))
            .ToList();

        BusinessType = monthlyReport.BusinessType;
        if (monthlyReport.DealerMonthlyReport is not null && BusinessType == EBusinessType.Dealer)
            DealerMonthlyReport = monthlyReport.DealerMonthlyReport;
        if (monthlyReport.ManufacturerMonthlyReport is not null && BusinessType == EBusinessType.Manufacturer)
            ManufacturerMonthlyReport = monthlyReport.ManufacturerMonthlyReport;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MonthlyReportResponse"/> class with additional customer and category information.
    /// </summary>
    /// <param name="monthlyReport">The monthly report entity.</param>
    /// <param name="customer">The customer entity.</param>
    public static MonthlyReportResponse Create(MonthlyReport monthlyReport, Customer customer)
    {
        var monthlyReportResponse = new MonthlyReportResponse(monthlyReport);

        monthlyReportResponse.Customer = new CustomerResponse(customer);
        return monthlyReportResponse;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MonthlyReportResponse"/> class with additional customer and user information.
    /// </summary>
    /// <param name="monthlyReport">The monthly report entity.</param>
    /// <param name="customer">The customer entity.</param>
    /// <param name="user">The user entity.</param>
    public static MonthlyReportResponse Create(MonthlyReport monthlyReport, Customer customer, User user)
    {
        var monthlyReportResponse = Create(monthlyReport, customer);
        monthlyReportResponse.User = new UserResponse(user);

        return monthlyReportResponse;
    }


}
