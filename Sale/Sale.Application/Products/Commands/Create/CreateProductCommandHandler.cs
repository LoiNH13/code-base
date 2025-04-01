using Application.Core.Abstractions.Data;
using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using Odoo.Domain.Repositories;
using Sale.Domain.Core.Errors;
using Sale.Domain.Entities.Products;
using Sale.Domain.Repositories;

namespace Sale.Application.Products.Commands.Create;

internal sealed class CreateProductCommandHandler(
    IProductRepository productRepository,
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork,
    IOdooProductRepository odooProductRepository)
    : ICommandHandler<CreateProductCommand, Result>
{
    public async Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        Maybe<Category> mbCategory = default!;
        if (request.CategoryId.HasValue)
        {
            mbCategory = await categoryRepository.GetByIdAsync(request.CategoryId ?? Guid.Empty);
            if (mbCategory.HasNoValue) return Result.Failure(SaleDomainErrors.Category.NotFound);
        }

        var odooProduct = await odooProductRepository.GetProductById(request.OdooRef);
        if (odooProduct.HasNoValue) return Result.Failure(SaleDomainErrors.Product.ProductMustInOdoo);

        var product = new Product(odooProduct.Value.DisplayName ?? request.Name, request.OdooRef, mbCategory,
            request.Weight, request.Price, odooProduct.Value.DefaultCode);

        productRepository.Insert(product);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}