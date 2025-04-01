using Application.Core.Abstractions.Data;
using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using Sale.Domain.Core.Errors;
using Sale.Domain.Entities.Products;
using Sale.Domain.Repositories;

namespace Sale.Application.ProductPrices.Commands.Update;

internal sealed class UpdateProductPriceCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateProductPriceCommand, Result>
{
    public async Task<Result> Handle(UpdateProductPriceCommand request, CancellationToken cancellationToken)
    {
        Maybe<Product> mbProduct = await productRepository.GetByIdAsync(request.ProductId);
        if (mbProduct.HasNoValue) return Result.Failure(SaleDomainErrors.Product.NotFound);

        Result result = mbProduct.Value.UpdatePrice(request.TimeFrameProductPriceId, request.Price);
        if (result.IsSuccess) await unitOfWork.SaveChangesAsync(cancellationToken);

        return result;
    }
}