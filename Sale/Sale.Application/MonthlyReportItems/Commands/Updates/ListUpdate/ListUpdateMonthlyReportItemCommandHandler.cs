using Application.Core.Abstractions.Data;
using Domain.Core.Primitives.Result;
using Sale.Domain.Core.Errors;
using Sale.Domain.Repositories;

namespace Sale.Application.MonthlyReportItems.Commands.Updates.ListUpdate;

internal sealed class ListUpdateMonthlyReportItemCommandHandler : ICommandHandler<ListUpdateMonthlyReportItemCommand, Result>
{
    readonly IUnitOfWork _unitOfWork;
    readonly IMonthlyReportRepository _monthlyReportRepository;

    public ListUpdateMonthlyReportItemCommandHandler(IUnitOfWork unitOfWork, IMonthlyReportRepository monthlyReportRepository)
    {
        _unitOfWork = unitOfWork;
        _monthlyReportRepository = monthlyReportRepository;
    }

    public async Task<Result> Handle(ListUpdateMonthlyReportItemCommand request, CancellationToken cancellationToken)
    {
        //get the monthly report
        var mbMonthlyReport = await _monthlyReportRepository.GetByIdAsync(request.MonthlyReportId);
        if (mbMonthlyReport.HasNoValue) return Result.Failure(SaleDomainErrors.MonthlyReport.NotFound);

        //update all items
        foreach (var item in request.Items)
        {
            var rs = mbMonthlyReport.Value.UpdateItem(item.Id, item.Quantity, item.Revenue, item.Note);
            if (rs.IsFailure) return rs;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
