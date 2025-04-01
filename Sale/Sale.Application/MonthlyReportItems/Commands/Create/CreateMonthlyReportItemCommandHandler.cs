using Application.Core.Abstractions.Data;
using Domain.Core.Primitives.Result;
using Sale.Domain.Core.Errors;
using Sale.Domain.Repositories;

namespace Sale.Application.MonthlyReportItems.Commands.Create;

internal sealed class CreateMonthlyReportItemCommandHandler(
    IMonthlyReportRepository monthlyReportRepository,
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateMonthlyReportItemCommand, Result>
{
    public async Task<Result> Handle(CreateMonthlyReportItemCommand request, CancellationToken cancellationToken)
    {
        //get id Monthly Report
        var mbMonthlyReport = await monthlyReportRepository.GetByIdAsync(request.MonthlyReportId);
        if (mbMonthlyReport.HasNoValue) return Result.Failure(SaleDomainErrors.MonthlyReport.NotFound);

        var mbCategory = await categoryRepository.GetByIdAsync(request.CategoryId);
        if (mbCategory.HasNoValue) return Result.Failure(SaleDomainErrors.Category.NotFound);

        Result result = mbMonthlyReport.Value.AddItem(mbCategory, request.Quantity, request.Revenue, request.Note, request.Group);
        if (result.IsFailure) return result;

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return result;
    }
}