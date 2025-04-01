using Application.Core.Extensions;
using Contract.Common;
using Domain.Core.Primitives.Maybe;
using Microsoft.EntityFrameworkCore;
using Sale.Contract.Metrics;
using Sale.Contract.Products;
using Sale.Contract.TimeFrames;
using Sale.Domain.Core.Abstractions;
using Sale.Domain.Entities;
using Sale.Domain.Entities.Customers;
using Sale.Domain.Entities.Metrics;
using Sale.Domain.Entities.Products;
using Sale.Domain.Repositories;

namespace Sale.Application.Metrics.Queries.Metrics.Forecast;

internal sealed class GetForecastMetricsQueryHandler(
    IProductRepository productRepository,
    ICustomerRepository customerRepository)
    : IQueryHandler<GetForecastMetricsQuery, Maybe<PagedList<GetMetricsResponse>>>
{
    public async Task<Maybe<PagedList<GetMetricsResponse>>> Handle(GetForecastMetricsQuery request,
        CancellationToken cancellationToken)
    {
        if (request.FromMY > request.ToMY) return Maybe<PagedList<GetMetricsResponse>>.None;

        IQueryable<Product> qrProduct = productRepository.Queryable()
            .Where(x => x.CategoryId == request.CategoryId)
            .Where(x => !x.Inactive)
            .WhereIf(request.ProductId.HasValue, x => x.Id == request.ProductId)
            .Include(x => x.ProductTimeFramePrices
                .Where(ptf =>
                    request.FromMY.TotalMonths <= (ptf.TimeFrame!.Month + (ptf.TimeFrame.Year * 12)) &&
                    request.ToMY.TotalMonths >= (ptf.TimeFrame.Month + (ptf.TimeFrame.Year * 12))))
            .Paginate(request.PageNumber, request.PageSize, out var paged);
        if (paged.NotExists()) return new PagedList<GetMetricsResponse>(paged);

        List<Product> products = await qrProduct.ToListAsync(cancellationToken: cancellationToken);

        Maybe<Customer> mbCustomer = await customerRepository.Queryable()
            .Where(customer => customer.Id == request.CustomerId) // Điều kiện lọc theo CustomerId
            .Include(customer => customer.CustomerTimeFrames
                .Where(ctf =>
                    request.FromMY.TotalMonths <= (ctf.TimeFrame!.Month + (ctf.TimeFrame.Year * 12)) &&
                    request.ToMY.TotalMonths >= (ctf.TimeFrame.Month + (ctf.TimeFrame.Year * 12)))
            )
            .ThenInclude(ctf => ctf.TimeFrame)
            .Include(customer => customer.CustomerTimeFrames)
            .ThenInclude(ctf => ctf.Metrics
                    .Where(m => products.Select(p => p.Id)
                        .Contains(m.ProductId)) // Chỉ lấy các sản phẩm trong qrProduct
            ).ThenInclude(metric => metric.ForeCast)
            .Include(customer => customer.CustomerTimeFrames).ThenInclude(x => x.Metrics)
            .ThenInclude(x => x.OriginalBudget)
            .Include(customer => customer.CustomerTimeFrames).ThenInclude(x => x.Metrics).ThenInclude(x => x.Target)
            .FirstOrDefaultAsync(cancellationToken) ?? default!;
        if (mbCustomer.HasNoValue) return Maybe<PagedList<GetMetricsResponse>>.None;

        List<CustomerTimeFrame> timeFrames = mbCustomer.Value.CustomerTimeFrames;
        var groupProducts = mbCustomer.Value.CustomerTimeFrames.SelectMany(x => x.Metrics).GroupBy(x => x.ProductId)
            .ToList();

        List<GetMetricsResponse> data = products.Select(p =>
        {
            List<TimeFrameMetricDto> timeFrameMetrics = groupProducts.Where(gp => gp.Key == p.Id)
                .SelectMany(x => x.Select(s =>
                {
                    TimeFrame timeFrame = timeFrames.Find(ctf => ctf.Id == s.CustomerTimeFrameId)?.TimeFrame!;
                    return new TimeFrameMetricDto
                    {
                        MetricId = s.Id,
                        OrderNumber = s.OrderNumber,
                        ReturnNumber = s.ReturnNumber,
                        OrderIds = s.OrderIds,
                        ReturnIds = s.ReturnIds,
                        CurrentPrice =
                            p.ProductTimeFramePrices.Find(price => price.TimeFrameId == timeFrame.Id)?.Price ??
                            p.Price,
                        ForeCast = GetMetricsResponse(s.ForeCast),
                        Target = GetMetricsResponse(s.Target),
                        OriginalBudget = GetMetricsResponse(s.OriginalBudget),
                        TimeFrame = new TimeFrameResponse(timeFrame)
                    };
                })).OrderBy(x => x.TimeFrame!.Year * 12 + x.TimeFrame.Month).ToList();

            return new GetMetricsResponse
            {
                Product = new ProductResponse(p, true),
                TimeFrameMetrics = timeFrameMetrics
            };
        }).OrderByDescending(x => x.Product!.Weight).ToList();

        return new PagedList<GetMetricsResponse>(paged, data);
    }

    private static MetricDetailResponse? GetMetricsResponse(IMetricAbstract? data)
    {
        if (data == null) return null;
        return new MetricDetailResponse(data);
    }
}