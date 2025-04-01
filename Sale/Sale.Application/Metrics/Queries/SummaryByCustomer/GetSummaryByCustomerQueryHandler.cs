using Domain.Core.Primitives.Maybe;
using Microsoft.EntityFrameworkCore;
using Sale.Contract.Categories;
using Sale.Contract.Metrics;
using Sale.Contract.TimeFrames;
using Sale.Domain.Entities.Customers;
using Sale.Domain.Entities.Products;
using Sale.Domain.Enumerations;
using Sale.Domain.Repositories;

namespace Sale.Application.Metrics.Queries.SummaryByCustomer;

internal sealed class GetSummaryByCustomerQueryHandler(
    ICustomerRepository customerRepository,
    ICategoryRepository categoryRepository)
    : IQueryHandler<GetSummaryByCustomerQuery, Maybe<List<SummaryByCustomerResponse>>>
{
    public async Task<Maybe<List<SummaryByCustomerResponse>>> Handle(GetSummaryByCustomerQuery request,
        CancellationToken cancellationToken)
    {
        IQueryable<Customer> query = customerRepository.Queryable()
            .Where(x => x.Id == request.CustomerId)
            .Include(customer => customer.CustomerTimeFrames
                .Where(ptf =>
                    request.FromMY.TotalMonths <= (ptf.TimeFrame!.Month + (ptf.TimeFrame.Year * 12)) &&
                    request.ToMY.TotalMonths >= (ptf.TimeFrame.Month + (ptf.TimeFrame.Year * 12))))
            .ThenInclude(x => x.TimeFrame)
            .Include(cus => cus.CustomerTimeFrames).ThenInclude(ctf => ctf.Metrics)
            .ThenInclude(metric => metric.Product).ThenInclude(product => product!.Category);

        query = request.MetricType switch
        {
            EMetricType.ForeCast => query.Include(cus => cus.CustomerTimeFrames)
                .ThenInclude(ctf => ctf.Metrics).ThenInclude(metric => metric.ForeCast),
            EMetricType.Target => query.Include(cus => cus.CustomerTimeFrames)
                .ThenInclude(ctf => ctf.Metrics).ThenInclude(metric => metric.Target),
            EMetricType.OriginalBudget => query.Include(cus => cus.CustomerTimeFrames)
                .ThenInclude(ctf => ctf.Metrics).ThenInclude(metric => metric.OriginalBudget),
            _ => query.Include(cus => cus.CustomerTimeFrames)
                .ThenInclude(ctf => ctf.Metrics)
        };

        Maybe<Customer> mbCustomer = await query.FirstOrDefaultAsync(cancellationToken) ?? default!;
        if (mbCustomer.HasNoValue) return default!;

        Maybe<List<Category>> mbCategories =
            await categoryRepository.Queryable().Where(x => x.IsShowSalePlan).ToListAsync(cancellationToken);
        if (mbCategories.HasNoValue) return default!;

        var groupCusCate = mbCustomer.Value.CustomerTimeFrames.SelectMany(x => x.Metrics)
            .GroupBy(x => x.Product!.Category);

        List<SummaryByCustomerResponse> response = mbCategories.Value.Select(cate =>
        {
            List<SummaryMetricByCategoryTimeFrame> cateTimeFrames = groupCusCate
                .Where(x => x.Key! == cate)
                .SelectMany(x => x)
                .GroupBy(x => x.CustomerTimeFrameId)
                .Select(x => new SummaryMetricByCategoryTimeFrame
                {
                    TimeFrame = new TimeFrameResponse(mbCustomer.Value.CustomerTimeFrames.Find(ctf => ctf.Id == x.Key)
                        ?.TimeFrame!),
                    TotalByCategoryTimeFrame = request.MetricType switch
                    {
                        EMetricType.OriginalBudget => x.Sum(metric => metric.OriginalBudget?.TotalAmount ?? 0),
                        EMetricType.ForeCast => x.Sum(metric => metric.ForeCast?.TotalAmount ?? 0),
                        EMetricType.Target => x.Sum(metric => metric.Target?.TotalAmount ?? 0),
                        _ => 0
                    }
                })
                .ToList();

            return new SummaryByCustomerResponse
            {
                Category = new CategoryResponse(cate),
                CategoryTimeFrames = cateTimeFrames
            };
        }).OrderByDescending(x => x.Category!.Weight).ToList();

        return response;
    }
}