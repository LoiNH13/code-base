using Application.Core.Abstractions.Data;
using Domain.Core.Primitives.Result;
using Sale.Domain.Core.Errors;
using Sale.Domain.Repositories;

namespace Sale.Application.MonthlyReportItems.Commands.Delete;

internal sealed class DeleteMonthlyReportItemCommandHandler(
    IMonthlyReportRepository monthlyReportRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteMonthlyReportItemCommand, Result>
{
    public async Task<Result> Handle(DeleteMonthlyReportItemCommand request, CancellationToken cancellationToken)
    {
        var mbMonthlyReport = await monthlyReportRepository.GetByIdAsync(request.MonthlyReportId);
        if (mbMonthlyReport.HasNoValue) return Result.Failure(SaleDomainErrors.MonthlyReport.NotFound);

        Result result = mbMonthlyReport.Value.RemoveItem(request.MonthlyReportItemId);
        if (result.IsFailure) return result;

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return result;
    }
}