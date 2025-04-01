using Domain.Core.Primitives.Result;
using Sale.Domain.Core.Abstractions;
using Sale.Domain.Core.Errors;
using Sale.Domain.Entities.MonthlyReports;
using Sale.Domain.Repositories;

namespace Sale.Domain.Services;

public class MonthlyReportService(
    ICustomerRepository customerRepository)
{
    public async Task<Result<MonthlyReport>> CreateMonthlyReport(DateTime fromTimeOnUtc,
        DateTime? toTimeOnUtc,
        IDynamicValue dynamicValue,
        string? note,
        int odooCustomerId)
    {
        //check customer exist if not then create
        var mbCustomer = await customerRepository.GetByOdooRefAsync(odooCustomerId);
        if (mbCustomer.HasNoValue) return Result.Failure<MonthlyReport>(SaleDomainErrors.Customer.NotFound);

        //check bussiness type has match between customer and dynamic value
        if (mbCustomer.Value.BusinessType != dynamicValue.GetBusinessType())
            mbCustomer.Value.UpdateBusinessType(dynamicValue.GetBusinessType());

        //create monthly report
        var monthlyReport = new MonthlyReport(mbCustomer, fromTimeOnUtc, toTimeOnUtc, dynamicValue, note);

        return Result.Success(monthlyReport);
    }
}