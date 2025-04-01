using Application.Core.Abstractions.Data;
using Domain.Core.Primitives.Result;
using Sale.Domain.Entities.Products;
using Sale.Domain.Repositories;

namespace Sale.Application.Categories.Commands.Create;

internal sealed class CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateCategoryCommand, Result>
{
    public async Task<Result> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        Category category = new Category(request.Name, request.OdooRef, request.Weight, request.IsShowSalePlan,
            request.IsShowMonthlyReport);

        categoryRepository.Insert(category);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}