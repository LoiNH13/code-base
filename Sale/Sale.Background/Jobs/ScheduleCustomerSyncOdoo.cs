using Application.Core.Abstractions.Common;
using Background.Settings;
using Domain.Core.Primitives.Maybe;
using Hangfire;
using Sale.Domain.Repositories;

namespace Sale.Background.Jobs;

public sealed class ScheduleCustomerSyncOdoo(ICustomerRepository customerRepository, IDateTime dateTime)
{
    private const double TotalTimeInSeconds = 3 * 60 * 60;

    public void Run() => ScheduleSync(dateTime.CurrentConvertMonths, false);

    public void RunWithLastMonth() => ScheduleSync(dateTime.CurrentConvertMonths - 1, true);

    private void ScheduleSync(int convertMonths, bool isLastMonth)
    {
        Maybe<Guid[]> customerIds = customerRepository.Queryable()
            .Where(x => x.CustomerTimeFrames.Any(ctf =>
                ctf.TimeFrame!.Year * 12 + ctf.TimeFrame!.Month == convertMonths))
            .Where(x => x.OdooRef != null && x.OdooRef > 0)
            .Select(x => x.Id)
            .ToArray();

        var intervalInSeconds = TotalTimeInSeconds / customerIds.Value.Length;

        for (var i = 0; i < customerIds.Value.Length; i++)
        {
            var index = i;
            BackgroundJob.Schedule<CustomerSyncOdoo>(
                BackgroundJobConst.Queues.Alpha,
                x => x.Run(customerIds.Value[index], convertMonths, isLastMonth),
                TimeSpan.FromSeconds(index * intervalInSeconds)
            );
        }
    }
}