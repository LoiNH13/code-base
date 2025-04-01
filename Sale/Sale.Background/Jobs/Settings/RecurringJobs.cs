namespace Sale.Background.Jobs.Settings;

public static class RecurringJobs
{
    internal static class OdooHostedServices
    {
        internal const string ProductsSyncOdoo = "ProductsSyncOdoo";

        internal const string CategoriesSyncOdoo = "CategoriesSyncOdoo";

        internal const string ScheduleCustomerSyncOdooRun = "ScheduleCustomerSyncOdooRun";

        internal const string ScheduleCustomerSyncOdooRunWithLastMonth = "ScheduleCustomerSyncOdooRunWithLastMonth";
    }

    internal static class MonthlyReportServices
    {
        internal const string LockMonthlyReport = "LockMonthlyReport";
    }
}