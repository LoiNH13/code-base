using Application.Core.Abstractions.Data;
using Domain.Core.Primitives.Result;
using Sale.Domain.Core.Errors;
using Sale.Domain.Repositories;

namespace Sale.Application.MonthlyReportItems.Commands.Updates.Update;

internal sealed class UpdateMonthlyReportItemCommandHandler(
    IUnitOfWork unitOfWork,
    IMonthlyReportRepository monthlyReportRepository)
    : ICommandHandler<UpdateMonthlyReportItemCommand, Result>
{
    public async Task<Result> Handle(UpdateMonthlyReportItemCommand request, CancellationToken cancellationToken)
    {
        var mbMonthlyReport = await monthlyReportRepository.GetByIdAsync(request.MonthlyReportId);
        if (mbMonthlyReport.HasNoValue) return Result.Failure(SaleDomainErrors.MonthlyReport.NotFound);

        Result result = mbMonthlyReport.Value.UpdateItem(request.MonthlyReportItemId,
            request.Quantity,
            request.Revenue,
            request.Note);
        if (result.IsFailure) return result;

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return result;
    }
}