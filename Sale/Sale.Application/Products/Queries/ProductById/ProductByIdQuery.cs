using Domain.Core.Primitives.Maybe;
using Sale.Contract.Products;

namespace Sale.Application.Products.Queries.ProductById;

public sealed class ProductByIdQuery : IQuery<Maybe<ProductResponse>>
{
    public Guid ProductId { get; }

    public ProductByIdQuery(Guid productId) => ProductId = productId;
}
