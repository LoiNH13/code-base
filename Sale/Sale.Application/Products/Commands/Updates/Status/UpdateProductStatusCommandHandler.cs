using Application.Core.Abstractions.Data;
using Domain.Core.Primitives.Result;
using Sale.Domain.Core.Errors;
using Sale.Domain.Repositories;

namespace Sale.Application.Products.Commands.Updates.Status;

internal sealed class UpdateProductStatusCommandHandler(
    IUnitOfWork unitOfWork,
    IProductRepository productRepository)
    : ICommandHandler<UpdateProductStatusCommand, Result>
{
    public async Task<Result> Handle(UpdateProductStatusCommand request, CancellationToken cancellationToken)
    {
        var mbProduct = await productRepository.GetByIdAsync(request.Id);
        if (mbProduct.HasNoValue) return Result.Failure(SaleDomainErrors.Product.NotFound);
        mbProduct.Value.ChangeStatus(!mbProduct.Value.Inactive);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}