namespace Sale.Contract.Customers;

public class CustomerTrackingRequest
{
    public int ConvertMonths { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public required List<Guid> ManagedByUserIds { get; set; }
    public bool IncludeSubordinateUsers { get; set; }
    public bool IsVisited { get; set; }
}