using Domain.Core.Abstractions;
using Domain.Core.Primitives;
using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using Sale.Domain.Core.Errors;

namespace Sale.Domain.Entities.Products;

public sealed class Product : AggregateRoot, IAuditableEntity
{
    public Product(string name, int odooRef, Category? category, int? weight, double price, string? odooCode) : base(Guid.NewGuid())
    {
        Name = name;
        OdooRef = odooRef;
        CategoryId = category?.Id;
        Weight = weight ?? 0;
        Price = price;
        OdooCode = odooCode;
    }

    public Guid? CategoryId { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public int OdooRef { get; private set; }

    public string? OdooCode { get; private set; }

    public double Price { get; private set; }

    public int Weight { get; private set; }

    public bool Inactive { get; private set; } = false;

    public DateTime CreatedOnUtc { get; }

    public DateTime? ModifiedOnUtc { get; }

    public Category? Category { get; }

    public List<ProductTimeFramePrice> ProductTimeFramePrices { get; private set; } = new();

    private Product() { }

    public double GetPriceByTimeFrame(TimeFrame timeFrame) => ProductTimeFramePrices.Find(x => x.TimeFrameId == timeFrame.Id)?.Price ?? Price;

    public void UpdateByJob(string? name, string? odooCode)
    {
        Name = name ?? Name;
        OdooCode = odooCode;
    }

    public Result Update(Guid? categoryId, string name, int odooRef, int? weight, double price, out bool changeOdooRef)
    {
        changeOdooRef = OdooRef != odooRef;
        CategoryId = categoryId;
        Weight = weight ?? 0;
        Name = name;
        OdooRef = odooRef;
        Price = price;
        return Result.Success();
    }

    public void ChangeStatus(bool status) => Inactive = status;

    public Result AddPrice(TimeFrame timeFrame, double price)
    {
        //check exist TimeFrame in Product
        Maybe<ProductTimeFramePrice> productTimeFramePrice = ProductTimeFramePrices.Find(x => x.TimeFrameId == timeFrame.Id) ?? default!;
        if (productTimeFramePrice.HasValue) return Result.Failure(SaleDomainErrors.ProductTimeFramePrice.TimeFrameExisted.AddParams($"{timeFrame.YearMonth}"));

        ProductTimeFramePrice newProductPrice = new ProductTimeFramePrice(this, timeFrame, price);
        ProductTimeFramePrices.Add(newProductPrice);
        return Result.Success();
    }

    public Result UpdatePrice(Guid priceId, double price)
    {
        Maybe<ProductTimeFramePrice> mbProductTimeFramePrice = ProductTimeFramePrices.Find(x => x.Id == priceId) ?? default!;
        if (mbProductTimeFramePrice.HasNoValue) return Result.Failure(SaleDomainErrors.ProductTimeFramePrice.NotFound);
        Result result = mbProductTimeFramePrice.Value.Update(price);
        return result;
    }

    public Result DeletePrice(Guid priceId)
    {
        Maybe<ProductTimeFramePrice> mbProductTimeFramePrice = ProductTimeFramePrices.Find(x => x.Id == priceId) ?? default!;
        if (mbProductTimeFramePrice.HasNoValue) return Result.Failure(SaleDomainErrors.ProductTimeFramePrice.NotFound);
        ProductTimeFramePrices.Remove(mbProductTimeFramePrice);
        return Result.Success();
    }
}
