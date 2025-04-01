using Application.Core.Abstractions.Factories;
using Domain.Core.Primitives.Maybe;
using Microsoft.EntityFrameworkCore;
using Odoo.Domain.Entities;
using Odoo.Domain.Repositories;
using Odoo.Persistence.Infrastructure;

namespace Odoo.Persistence.Repositories;

internal sealed class OdooOrderRepository : IOdooOrderRepository
{
    readonly OdooDbContext _context;

    public OdooOrderRepository(OdooDbContext context, IDistributedCacheFactory cacheFactory)
    {
        _context = context;
        cacheFactory.CreateCache(Const.CacheInstanceName);
    }

    public async Task<Maybe<ReportSaleOrderView>> GetReportSaleOrderViewByOrderName(string orderName) =>
        await _context.ReportSaleOrderViews.FirstOrDefaultAsync(x => x.OrderName == orderName) ?? default!;

    public async Task<List<ReportSaleOrderView>> NotPaidSOViewByInvoices(string? internalRef, List<string>? invoices)
    {
        List<string> filters = ["amount_total_invoice > amount_paid_invoice"];
        if (!string.IsNullOrWhiteSpace(internalRef)) filters.Add($"internal_ref = '{internalRef}'");
        if ((invoices?.Count ?? 0) != 0)
            filters.Add($"upper(invoice_ref) similar to '%({string.Join("|", invoices!).ToUpper()})%%'");

        string query = $"SELECT * FROM report_sale_order_view WHERE {string.Join(" AND ", filters)}";
        return await _context.ReportSaleOrderViews.FromSqlRaw(query).ToListAsync();
    }

    public IQueryable<SaleOrder> Queryable() => _context.SaleOrders.AsQueryable();

    public async Task<List<SaleReport>> SaleReportsByMonth(int odooCustomerId, int convertMonths) =>
        await _context.SaleReports.Where(x => x.PartnerId == odooCustomerId && x.State == "sale")
            .Where(x => x.Date!.Value.Year * 12 + x.Date.Value.Month == convertMonths)
            .Where(x => x.CategId != 558 && x.CategId != 888)
            .ToListAsync();

    public IQueryable<ReportSaleOrderView> ViewQueryable() => _context.ReportSaleOrderViews;
}