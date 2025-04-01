using Domain.Core.Primitives;

namespace Sale.Contract.Views;

public sealed class CusAgMonthlyView : Entity
{
    public Guid? MonthlyReportId { get; set; }

    public Guid? ManagedByUserId { get; set; }

    public int? OdooRef { get; set; }

    public string? Name { get; set; }

    public DateTime? FromTimeOnUtc { get; set; }
}
