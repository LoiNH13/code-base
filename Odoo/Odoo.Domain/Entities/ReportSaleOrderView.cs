using System;
using System.Collections.Generic;

namespace Odoo.Domain.Entities;

public partial class ReportSaleOrderView
{
    public int? Id { get; set; }

    public int? OrderId { get; set; }

    public string? CompanyAnalyticAccount { get; set; }

    public string? OrderName { get; set; }

    public string? InvoiceRef { get; set; }

    public string? SoType { get; set; }

    public int? CustomerId { get; set; }

    public string? CustomerName { get; set; }

    public string? InternalRef { get; set; }

    public string? ClientOrderRef { get; set; }

    public string? PaymentTerm { get; set; }

    public string? StateName { get; set; }

    public string? PickingStatus { get; set; }

    public string? SoReferenceNumber { get; set; }

    public string? Warehouse { get; set; }

    public int? TeamId { get; set; }

    public string? Team { get; set; }

    public string? Channel { get; set; }

    public string? Responsible { get; set; }

    public DateTime? DateDones { get; set; }

    public int? SalespersonId { get; set; }

    public string? Salesperson { get; set; }

    public string? ShippingType { get; set; }

    public decimal? AmountTotalInvoice { get; set; }

    public decimal? AmountPaidInvoice { get; set; }

    public DateOnly? PaymentDueDate { get; set; }

    public DateOnly? DateOrder { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? WriteDate { get; set; }

    public DateOnly? DateInvoices { get; set; }

    public DateOnly? DateDues { get; set; }

    public string? State { get; set; }
}
