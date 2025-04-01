using Application.Core.Abstractions.Common;
using Application.Core.Abstractions.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Odoo.Domain.Repositories;
using Sale.Domain.Repositories;

namespace Sale.Background.Jobs;

internal sealed class ProductsSyncOdoo(
    IOdooProductRepository odooProductRepository,
    IProductRepository productRepository,
    ILogger<ProductsSyncOdoo> logger,
    IUnitOfWork unitOfWork,
    IDateTime dateTime)
{
    public async Task Run()
    {
        await using var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            var odooProducts = await odooProductRepository.Queryable(string.Empty)
                .Where(p => p.WriteDate > DateTime.SpecifyKind(dateTime.UtcNow.AddDays(-1), DateTimeKind.Unspecified))
                .ToListAsync();

            if (odooProducts.Count > 0)
            {
                var odooProductIds = odooProducts.Select(p => p.Id).ToHashSet();
                var products = await productRepository.Queryable()
                    .Where(p => odooProductIds.Contains(p.OdooRef))
                    .ToListAsync();

                if (products.Count > 0)
                {
                    var odooProductDict = odooProducts.ToDictionary(p => p.Id);
                    foreach (var product in products)
                        if (odooProductDict.TryGetValue(product.OdooRef, out var odooProduct))
                            product.UpdateByJob(odooProduct.DisplayName, odooProduct.DefaultCode);

                    await unitOfWork.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            logger.LogError(ex, "Failed to sync products from Odoo: {ErrorMessage}", ex.Message);
        }
    }
}