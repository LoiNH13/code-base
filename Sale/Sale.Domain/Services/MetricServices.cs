using Domain.Core.Utility;
using Sale.Domain.Entities.Metrics;

namespace Sale.Domain.Services;

public sealed class MetricServices
{
    private MetricServices()
    {
    }

    public static void UpdateOdooNumber(Metric metric, double orderNumber, double returnNumber,
        List<int> orderNumberIds, List<int> returnNumberIds, bool isUpdateWholeSale)
    {
        metric.UpdateOdooNumber(orderNumber, returnNumber, orderNumberIds, returnNumberIds);
        if (isUpdateWholeSale) metric.ForeCast!.UpdateWholeSales(orderNumber);
    }

    internal static void ReCalculateMetrics(List<CustomerTimeFrame> customerTimeFrames)
    {
        IDictionary<Guid, Metric> lastMetricProducts = new Dictionary<Guid, Metric>();

        foreach (var item in customerTimeFrames.OrderBy(x => x.TimeFrame!.ConvertMonths))
        {
            foreach (var metric in item.Metrics)
            {
                if (lastMetricProducts.ContainsKey(metric.ProductId))
                {
                    Calculate2Metrics(lastMetricProducts[metric.ProductId], metric);
                    lastMetricProducts[metric.ProductId] = metric;
                }
                else
                {
                    metric.ReCalculateForecast();
                    lastMetricProducts.Add(metric.ProductId, metric);
                }
            }
        }
    }

    private static void Calculate2Metrics(Metric lastMetric, Metric currentMetrics)
    {
        Ensure.NotFalse(() => lastMetric.ProductId == currentMetrics.ProductId, "Product Id is not match",
            nameof(lastMetric.ProductId));

        currentMetrics.ForeCast!.UpdateLastStock(lastMetric.ForeCast!.StockNumber);
        currentMetrics.ReCalculateForecast();
    }

    //make a function clean forecast and recalculate
    //input is a list of metric
}