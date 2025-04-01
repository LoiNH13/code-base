using Application.Core.Abstractions.Common;
using Application.Core.Abstractions.Data;
using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using Microsoft.EntityFrameworkCore;
using Sale.Domain.Core.Errors;
using Sale.Domain.Entities;
using Sale.Domain.Entities.Customers;
using Sale.Domain.Entities.Products;
using Sale.Domain.Repositories;
using Sale.Domain.Services;

namespace Sale.Application.Metrics.Commands.AddsOrUpdates;

internal sealed class AddOrUpdateMetricsCommandHandler(
    ICustomerRepository customerRepository,
    IProductRepository productRepository,
    ITimeFrameRepository timeFrameRepository,
    IUnitOfWork unitOfWork,
    IDateTime dateTime)
    : ICommandHandler<AddOrUpdateMetricsCommand, Result>
{
    public async Task<Result> Handle(AddOrUpdateMetricsCommand request, CancellationToken cancellationToken)
    {
        Maybe<Customer> mbCustomer = await customerRepository.Queryable().Where(x => x.Id == request.CustomerId)
            .Include(x => x.CustomerTimeFrames).ThenInclude(x => x.Metrics).ThenInclude(x => x.ForeCast)
            .Include(x => x.CustomerTimeFrames).ThenInclude(x => x.Metrics).ThenInclude(x => x.Product)
            .Include(x => x.CustomerTimeFrames).ThenInclude(x => x.TimeFrame)
            .FirstOrDefaultAsync(cancellationToken) ?? default!;
        if (mbCustomer.HasNoValue) return Result.Failure(SaleDomainErrors.Customer.NotFound);

        Maybe<List<TimeFrame>> mbTimeFrames = await timeFrameRepository.Queryable()
            .Where(x => request.GetTimeFrameIds().Contains(x.Id))
            .ToListAsync(cancellationToken: cancellationToken);
        if (mbTimeFrames.HasNoValue) return Result.Failure(SaleDomainErrors.TimeFrame.NotFound);

        Maybe<List<Product>> mbProducts = await productRepository.Queryable()
            .Where(x => request.MetricProducts.Select(s => s.ProductId).Contains(x.Id))
            .Include(
                x => x.ProductTimeFramePrices.Where(price => request.GetTimeFrameIds().Contains(price.TimeFrameId)))
            .ToListAsync(cancellationToken: cancellationToken);
        if (mbProducts.HasNoValue) return Result.Failure(SaleDomainErrors.Product.NotFound);

        foreach (var product in request.MetricProducts)
        {
            Maybe<Product> mbProduct = mbProducts.Value.Find(x => x.Id == product.ProductId) ?? default!;
            if (mbProduct.HasNoValue) return Result.Failure(SaleDomainErrors.Product.NotFound);

            foreach (var timeFrame in product.TimeFrameMetrics!)
            {
                Maybe<TimeFrame> mbTimeFrame = mbTimeFrames.Value.Find(x => x.Id == timeFrame.TimeFrameId) ?? default!;
                if (mbTimeFrame.HasNoValue) return Result.Failure(SaleDomainErrors.TimeFrame.NotFound);

                Result result = CustomerServices.CreateOrUpdateMetric(mbCustomer,
                    mbProduct,
                    mbTimeFrame,
                    timeFrame.LastStockNumber,
                    timeFrame.WholeSalesNumber,
                    timeFrame.RetailSalesNumber,
                    timeFrame.StockNumber);
                if (result.IsFailure) return result;
            }
        }

        mbCustomer.Value.CalculateMetricsLogic(dateTime.CurrentConvertMonths - 1);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}