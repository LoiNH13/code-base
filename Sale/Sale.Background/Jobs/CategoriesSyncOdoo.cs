using Application.Core.Abstractions.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Odoo.Domain.Repositories;
using Sale.Domain.Repositories;

namespace Sale.Background.Jobs;

internal sealed class CategoriesSyncOdoo(
    ICategoryRepository categoryRepository,
    IOdooCategoryRepository odooCategoryRepository,
    ILogger<CategoriesSyncOdoo> logger,
    IUnitOfWork unitOfWork)
{
    public async Task Run()
    {
        await using var transaction = await unitOfWork.BeginTransactionAsync();
        try
        {
            var categories = await categoryRepository.Queryable()
                .Where(x => (x.OdooRef ?? 0) > 0)
                .ToListAsync();

            if (categories.Count > 0)
            {
                var odooRefIds = categories
                    .Select(c => c.OdooRef ?? 0).ToHashSet();
                var odooCategories = await odooCategoryRepository.Queryable()
                    .Where(x => odooRefIds.Contains(x.Id))
                    .ToListAsync();

                if (odooCategories.Count > 0)
                {
                    var odooCategoryDict = odooCategories.ToDictionary(c => c.Id);
                    foreach (var category in categories)
                        if (odooCategoryDict.TryGetValue(category.OdooRef ?? 0, out var odooCategory))
                            category.UpdateByJob(odooCategory.Name);

                    await unitOfWork.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            logger.LogError(ex, "Error during category synchronization: {ErrorMessage}", ex.Message);
        }
    }
}