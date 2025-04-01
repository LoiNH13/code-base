namespace Payment.Persistence.Models;

public partial class PaymentSm
{
    public int Id { get; set; }

    public int? Smsid { get; set; }

    public string Method { get; set; } = null!;

    public DateTime Smsdate { get; set; }

    public string Smscontent { get; set; } = null!;

    public decimal Smsamount { get; set; }

    public string SmsbankName { get; set; } = null!;

    public string SmsbankNumber { get; set; } = null!;

    public string Smsphone { get; set; } = null!;

    public string? LbsuserName { get; set; }

    public string? Lbsplate { get; set; }

    public string? LbsdocumentCode { get; set; }

    public string? Lbsmodified { get; set; }

    public string? OdooSonumber { get; set; }

    public string? OdooCustomerCode { get; set; }

    public string? OdooCustomerName { get; set; }

    public string? OdooModified { get; set; }

    public string? SimStatusName { get; set; }

    public string? SimModified { get; set; }

    public string? SignalStatusName { get; set; }

    public string? SignalModified { get; set; }

    public string? AccountantNote { get; set; }

    public string? AccountantNoteModified { get; set; }

    public string? InboundPaymentNumber { get; set; }

    public bool? InboundPaymentIsCreated { get; set; }

    public string? InboundPaymentModified { get; set; }

    public sbyte? InboundPaymentIsConfirm { get; set; }

    public DateTime? InboundPaymentDate { get; set; }

    public string? OtherRemark { get; set; }

    public string? OtherRemarkModified { get; set; }

    public string? ObjectType { get; set; }

    public string? ActionBy { get; set; }

    public int? BranchId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? Vaaccount { get; set; }

    public int? SaleId { get; set; }

    public string? SaleName { get; set; }

    public int? InboundPaymentId { get; set; }

}
