using Application.Core.Extensions;
using Contract.Common;
using Domain.Core.Primitives.Maybe;
using Microsoft.EntityFrameworkCore;
using Sale.Contract.Categories;
using Sale.Domain.Entities.Products;
using Sale.Domain.Repositories;

namespace Sale.Application.Categories.Queries.Categories;

internal sealed class CategoriesQueryHandler(ICategoryRepository categoryRepository)
    : ICommandHandler<CategoriesQuery, Maybe<PagedList<CategoryResponse>>>
{
    public async Task<Maybe<PagedList<CategoryResponse>>> Handle(CategoriesQuery request,
        CancellationToken cancellationToken)
    {
        IQueryable<Category> query = categoryRepository.Queryable(request.SearchText)
            .WhereIf(request.ShowInApp is EShowInApp.SalePlan, x => x.IsShowSalePlan)
            .WhereIf(request.ShowInApp is EShowInApp.MonthlyReport, x => x.IsShowMonthlyReport)
            .Paginate(request.PageNumber, request.PageSize, out var page);

        if (page.NotExists()) return new PagedList<CategoryResponse>(page);

        List<CategoryResponse> data = await query.Select(x => new CategoryResponse(x)).ToListAsync(cancellationToken);

        return new PagedList<CategoryResponse>(page, data);
    }
}