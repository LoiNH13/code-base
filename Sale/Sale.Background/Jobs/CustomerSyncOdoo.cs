using Application.Core.Abstractions.Data;
using Domain.Core.Primitives.Maybe;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Odoo.Domain.Entities;
using Odoo.Domain.Repositories;
using Sale.Contract.Odoo.Customers;
using Sale.Domain.Entities.Customers;
using Sale.Domain.Repositories;
using Sale.Domain.Services;

namespace Sale.Background.Jobs;

public sealed class CustomerSyncOdoo(
    ICustomerRepository customerRepository,
    IOdooOrderRepository odooOrderRepository,
    IOdooCustomerRepository odooCustomerRepository,
    IUnitOfWork unitOfWork,
    ILogger<CustomerSyncOdoo> logger)
{
    public async Task Run(Guid customerId, int convertMonths, bool isUpdateWholeSale)
    {
        await using var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            var mbCustomer = await GetCustomerWithDetails(customerId, convertMonths);
            if (mbCustomer.HasNoValue || (mbCustomer.Value.OdooRef ?? 0) == 0) return;

            var customer = mbCustomer.Value;
            var mbOdooCustomerResponse = await GetOdooCustomerResponse(customer.OdooRef ?? 0);
            if (mbOdooCustomerResponse.HasNoValue) return;

            customer.UpdateByEvent(mbOdooCustomerResponse.Value.DTWithName);

            var saleReports = await odooOrderRepository.SaleReportsByMonth(customer.OdooRef ?? 0, convertMonths);
            if (saleReports.Count > 0)
            {
                ProcessSaleReports(customer, saleReports, convertMonths, isUpdateWholeSale);
            }

            customer.CalculateMetricsLogic(convertMonths);

            await unitOfWork.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            logger.LogError(ex, ex.ToString());
        }
    }

    private async Task<Maybe<Customer>> GetCustomerWithDetails(Guid customerId, int convertMonths)
    {
        return await customerRepository.QueryableSplitQuery()
            .Include(x =>
                x.CustomerTimeFrames.Where(ctf => ctf.TimeFrame!.Year * 12 + ctf.TimeFrame!.Month >= convertMonths))
            .ThenInclude(x => x.Metrics).ThenInclude(x => x.ForeCast)
            .Include(x => x.CustomerTimeFrames).ThenInclude(x => x.TimeFrame)
            .Include(x => x.CustomerTimeFrames).ThenInclude(x => x.Metrics).ThenInclude(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == customerId) ?? default!;
    }

    private async Task<Maybe<OdooCustomerResponse>> GetOdooCustomerResponse(int odooRef) =>
        await odooCustomerRepository.GetCustomerById(odooRef)
            .Match(x => Maybe<OdooCustomerResponse>.From(new OdooCustomerResponse
            {
                Id = x.Id,
                Name = x.Name,
                InternalRef = x.Ref
            }), () => default!);

    private static void ProcessSaleReports(Customer customer, IEnumerable<SaleReport> saleReports, int convertMonths,
        bool isUpdateWholeSale)
    {
        var groups = saleReports.GroupBy(x => x.ProductId).ToList();
        var metrics = customer.CustomerTimeFrames
            .Where(x => x.TimeFrame!.ConvertMonths == convertMonths)
            .SelectMany(x => x.Metrics)
            .ToList();

        foreach (var metric in metrics)
        {
            var group = groups.Where(x => x.Key == metric.Product!.OdooRef)
                .SelectMany(x => x).ToList();

            var (sale, saleIds) = GetSaleInfo(group);
            var (@return, returnIds) = GetReturnInfo(group);

            MetricServices.UpdateOdooNumber(metric, sale, @return, saleIds, returnIds, isUpdateWholeSale);
        }
    }

    private static (double Sale, List<int> SaleIds) GetSaleInfo(List<SaleReport> group)
    {
        var saleItems = group.Where(x => x.Name!.StartsWith("SO")).ToList();
        double sale = (double)saleItems.Sum(x => x.ProductUomQty)!;
        var saleIds = saleItems.Select(x => x.Id ?? 0).Distinct().ToList();
        return (sale, saleIds);
    }

    private static (double Return, List<int> ReturnIds) GetReturnInfo(List<SaleReport> group)
    {
        var returnItems = group.Where(x => x.Name!.StartsWith("RSO")).ToList();
        double @return = (double)returnItems.Sum(x => Math.Abs(x.ProductUomQty ?? 0));
        var returnIds = returnItems.Select(x => x.Id ?? 0).Distinct().ToList();
        return (@return, returnIds);
    }
}