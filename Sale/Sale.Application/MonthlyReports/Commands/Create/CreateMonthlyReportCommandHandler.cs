using Application.Core.Abstractions.Data;
using Domain.Core.Primitives.Result;
using Sale.Contract.MonthlyReports;
using Sale.Domain.Core.Errors;
using Sale.Domain.Entities.MonthlyReports;
using Sale.Domain.Entities.Products;
using Sale.Domain.Enumerations;
using Sale.Domain.Repositories;
using Sale.Domain.Services;

namespace Sale.Application.MonthlyReports.Commands.Create;

internal sealed class CreateMonthlyReportCommandHandler(
    IMonthlyReportRepository monthlyReportRepository,
    ICategoryRepository categoryRepository,
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateMonthlyReportCommand, Result<CreateMonthlyReportResponse>>
{
    public async Task<Result<CreateMonthlyReportResponse>> Handle(CreateMonthlyReportCommand request,
        CancellationToken cancellationToken)
    {
        var monthlyReportService = new MonthlyReportService(customerRepository);
        Result<MonthlyReport> rsMonthlyReport = await monthlyReportService.CreateMonthlyReport(request.FromTimeOnUtc,
            request.ToTimeOnUtc,
            request.DynamicValue,
            request.Note,
            request.OdooCustomerId);

        if (rsMonthlyReport.IsFailure) return Result.Failure<CreateMonthlyReportResponse>(rsMonthlyReport.Error);
        if (request.Items != null)
        {
            List<Category> categories = categoryRepository.Queryable()
                .Where(x => request.Items.Select(x => x.CategoryId).Distinct().ToList().Contains(x.Id))
                .ToList();
            foreach (var item in request.Items)
            {
                var category = categories.FirstOrDefault(x => x.Id == item.CategoryId);
                if (category is null) return Result.Failure<CreateMonthlyReportResponse>(SaleDomainErrors.Category.NotFound);
                var rsAddItem = rsMonthlyReport.Value.AddItem(category, item.Quantity, item.Revenue, item.Note, item.Group ?? EMonthlyReportItem.Default);
                if (rsAddItem.IsFailure) return Result.Failure<CreateMonthlyReportResponse>(rsAddItem.Error);
            }
        }

        monthlyReportRepository.Insert(rsMonthlyReport.Value);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(new CreateMonthlyReportResponse(rsMonthlyReport.Value.Id));
    }
}