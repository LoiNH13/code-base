using Domain.Core.Primitives.Maybe;
using Odoo.Domain.Entities;

namespace Odoo.Domain.Repositories;

public interface IOdooOrderRepository
{
    IQueryable<SaleOrder> Queryable();

    IQueryable<ReportSaleOrderView> ViewQueryable();

    Task<Maybe<ReportSaleOrderView>> GetReportSaleOrderViewByOrderName(string orderName);

    Task<List<SaleReport>> SaleReportsByMonth(int odooCustomerId, int convertMonths);

    Task<List<ReportSaleOrderView>> NotPaidSOViewByInvoices(string? internalRef, List<string>? invoices);
}