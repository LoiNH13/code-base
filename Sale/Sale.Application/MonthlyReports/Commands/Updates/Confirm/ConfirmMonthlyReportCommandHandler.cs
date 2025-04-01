using Application.Core.Abstractions.Data;
using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using Sale.Domain.Core.Errors;
using Sale.Domain.Entities.MonthlyReports;
using Sale.Domain.Repositories;

namespace Sale.Application.MonthlyReports.Commands.Updates.Confirm;

internal sealed class ConfirmMonthlyReportCommandHandler(
    IMonthlyReportRepository monthlyReportRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<ConfirmMonthlyReportCommand, Result>
{
    public async Task<Result> Handle(ConfirmMonthlyReportCommand request, CancellationToken cancellationToken)
    {
        Maybe<MonthlyReport> mbMonthlyReport = await monthlyReportRepository.GetByIdAsync(request.MonthlyReportId);
        if (mbMonthlyReport.HasNoValue) return Result.Failure(SaleDomainErrors.MonthlyReport.NotFound);

        Result result = mbMonthlyReport.Value.Confirm();
        if (result.IsFailure) return result;

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}