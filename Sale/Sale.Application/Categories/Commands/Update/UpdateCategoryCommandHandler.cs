using Application.Core.Abstractions.Data;
using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using Sale.Domain.Core.Errors;
using Sale.Domain.Entities.Products;
using Sale.Domain.Repositories;

namespace Sale.Application.Categories.Commands.Update;

internal sealed class UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateCategoryCommand, Result>
{
    public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        Maybe<Category> mbCategory = await categoryRepository.GetByIdAsync(request.Id);
        if (mbCategory.HasNoValue) return Result.Failure(SaleDomainErrors.Category.NotFound);

        Result categoryResult = mbCategory.Value.Update(request.Name, request.OdooRef, request.Weight,
            request.IsShowSalePlan, request.IsShowMonthlyReport);
        if (categoryResult.IsFailure) return Result.Failure(categoryResult.Error);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}