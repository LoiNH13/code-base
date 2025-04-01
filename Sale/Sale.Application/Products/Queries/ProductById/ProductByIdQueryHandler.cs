using Domain.Core.Primitives.Maybe;
using Sale.Contract.Products;
using Sale.Domain.Repositories;

namespace Sale.Application.Products.Queries.ProductById;

internal sealed class ProductByIdQueryHandler(IProductRepository productRepository)
    : IQueryHandler<ProductByIdQuery, Maybe<ProductResponse>>
{
    public async Task<Maybe<ProductResponse>> Handle(ProductByIdQuery request, CancellationToken cancellationToken) =>
        await productRepository.GetByIdAsync(request.ProductId).Match(x => new ProductResponse(x), () => default!);
}