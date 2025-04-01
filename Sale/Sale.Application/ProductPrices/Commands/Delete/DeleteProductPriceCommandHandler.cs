using Application.Core.Abstractions.Data;
using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using Sale.Domain.Core.Errors;
using Sale.Domain.Entities.Products;
using Sale.Domain.Repositories;

namespace Sale.Application.ProductPrices.Commands.Delete;

public sealed class DeleteProductPriceCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteProductPriceCommand, Result>
{
    public async Task<Result> Handle(DeleteProductPriceCommand request, CancellationToken cancellationToken)
    {
        Maybe<Product> mbProduct = await productRepository.GetByIdAsync(request.ProductId);
        if (mbProduct.HasNoValue) return Result.Failure(SaleDomainErrors.Product.NotFound);

        Result result = mbProduct.Value.DeletePrice(request.TimeFrameProductPriceId);

        if (result.IsSuccess) await unitOfWork.SaveChangesAsync(cancellationToken);

        return result;
    }
}