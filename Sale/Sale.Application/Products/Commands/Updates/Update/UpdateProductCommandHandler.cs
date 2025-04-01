using Application.Core.Abstractions.Data;
using Domain.Core.Primitives.Result;
using Odoo.Domain.Repositories;
using Sale.Domain.Core.Errors;
using Sale.Domain.Repositories;

namespace Sale.Application.Products.Commands.Updates.Update;

internal sealed class UpdateProductCommandHandler(
    IProductRepository productRepository,
    IOdooProductRepository odooProductRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateProductCommand, Result>
{
    public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var mbProduct = await productRepository.GetByIdAsync(request.ProductId);
        if (mbProduct.HasNoValue) return Result.Failure(SaleDomainErrors.Product.NotFound);

        var result = mbProduct.Value.Update(request.CategoryId, request.ProductName, request.OdooRef, request.Weight,
            request.Price, out var changeOdooRef);
        if (result.IsFailure) return result;
        if (changeOdooRef)
        {
            var mbOdooProduct = await odooProductRepository.GetProductById(request.OdooRef);
            if (mbOdooProduct.HasNoValue) return Result.Failure(SaleDomainErrors.Product.ProductMustInOdoo);
            mbProduct.Value.UpdateByJob(mbOdooProduct.Value.DisplayName, mbOdooProduct.Value.DefaultCode);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return result;
    }
}