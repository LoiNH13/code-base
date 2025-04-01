using Domain.Core.Primitives.Result;

namespace Sale.Application.Products.Commands.Create;

public sealed class CreateProductCommand : ICommand<Result>
{
    public CreateProductCommand(Guid? categoryId, string name, int odooRef, int? weight, double price)
    {
        CategoryId = categoryId;
        Name = name;
        OdooRef = odooRef;
        Weight = weight;
        Price = price;
    }

    public Guid? CategoryId { get; }

    public string Name { get; }

    public int OdooRef { get; }

    public int? Weight { get; }

    public double Price { get; }
}
