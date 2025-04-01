using Application.Core.Abstractions.Data;
using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using Sale.Domain.Core.Errors;
using Sale.Domain.Entities;
using Sale.Domain.Entities.Products;
using Sale.Domain.Repositories;

namespace Sale.Application.ProductPrices.Commands.Create;

internal sealed class CreateProductPriceCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork,
    ITimeFrameRepository timeFrameRepository)
    : ICommandHandler<CreateProductPriceCommand, Result>
{
    public async Task<Result> Handle(CreateProductPriceCommand request, CancellationToken cancellationToken)
    {
        Maybe<Product> mbProduct = await productRepository.GetByIdAsync(request.ProductId);
        if (mbProduct.HasNoValue) return Result.Failure(SaleDomainErrors.Product.NotFound);

        Maybe<TimeFrame> mbTimeFrame = await timeFrameRepository.GetByIdAsync(request.TimeFrameId);
        if (mbTimeFrame.HasNoValue) return Result.Failure(SaleDomainErrors.TimeFrame.NotFound);

        Result result = mbProduct.Value.AddPrice(mbTimeFrame, request.Price);
        if (result.IsSuccess) await unitOfWork.SaveChangesAsync(cancellationToken);

        return result;
    }
}