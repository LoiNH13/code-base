using Domain.Core.Primitives.Result;

namespace Sale.Application.Products.Commands.Updates.Update;

public sealed class UpdateProductCommand : ICommand<Result>
{
    public UpdateProductCommand(Guid productId, string productName, int odooRef, Guid categoryId, int? weight, double price)
    {
        ProductId = productId;
        ProductName = productName;
        OdooRef = odooRef;
        CategoryId = categoryId;
        Weight = weight;
        Price = price;
    }

    public Guid ProductId { get; }

    public string ProductName { get; }

    public int OdooRef { get; }

    public Guid CategoryId { get; }

    public int? Weight { get; }

    public double Price { get; }
}
