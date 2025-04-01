using Application.Core.Abstractions.Data;
using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using Sale.Domain.Core.Errors;
using Sale.Domain.Entities.MonthlyReports;
using Sale.Domain.Repositories;

namespace Sale.Application.MonthlyReports.Commands.Updates.Update;

internal sealed class UpdateMonthlyReportCommandHandler(
    IMonthlyReportRepository monthlyReportRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateMonthlyReportCommand, Result>
{
    public async Task<Result> Handle(UpdateMonthlyReportCommand request, CancellationToken cancellationToken)
    {
        Maybe<MonthlyReport> mbMonthlyReport = await monthlyReportRepository.GetByIdAsync(request.MonthlyReportId);
        if (mbMonthlyReport.HasNoValue) return Result.Failure(SaleDomainErrors.MonthlyReport.NotFound);

        Result result = mbMonthlyReport.Value.Update(request.FromTimeOnUtc,
            request.ToTimeOnUtc,
            request.DynamicData,
            request.Note);
        if (result.IsFailure) return result;

        //update all items
        foreach (var item in request.Items)
        {
            var rs = mbMonthlyReport.Value.UpdateItem(item.Id, item.Quantity, item.Revenue, item.Note);
            if (rs.IsFailure) return rs;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return result;
    }
}