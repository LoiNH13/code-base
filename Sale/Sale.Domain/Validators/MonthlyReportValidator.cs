using Domain.Core.Primitives.Result;
using Sale.Domain.Core.Errors;
using Sale.Domain.Entities.MonthlyReports;

namespace Sale.Domain.Validators;

internal static class MonthlyReportValidator
{
    internal static Result CheckFromToValid(this MonthlyReport monthlyReport, out Result result)
    {
        if (monthlyReport.FromTimeOnUtc > monthlyReport.ToTimeOnUtc)
        {
            return result = Result.Failure(SaleDomainErrors.MonthlyReport.FromThanToTime);
        }
        else if ((monthlyReport.ToTimeOnUtc - monthlyReport.FromTimeOnUtc) > TimeSpan.FromDays(1))
        {
            return result = Result.Failure(SaleDomainErrors.MonthlyReport.TimeMustWithInDay);
        }
        return result = Result.Success();
    }
}
