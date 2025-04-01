using Domain.Core.Primitives.Result;
using Sale.Domain.Entities;
using Sale.Domain.Entities.Customers;
using Sale.Domain.Entities.Metrics;
using Sale.Domain.Entities.Products;
using Sale.Domain.Enumerations;

namespace Sale.Domain.Services;

public static class CustomerServices
{
    /// <summary>
    /// Creates a new plan customer with the given code, name, and location information.
    /// </summary>
    /// <param name="code">The code for the customer.</param>
    /// <param name="name">The name of the customer.</param>
    /// <param name="cityId">The ID of the city where the customer is located.</param>
    /// <param name="districtId">The ID of the district where the customer is located.</param>
    /// <param name="wardId">The ID of the ward where the customer is located.</param>
    /// <param name="actionByUser">The ID of the user who is creating the customer.</param>
    /// <returns>A <see cref="Result{Customer}"/> containing the created customer if successful, or a <see cref="Result"/> with an error if unsuccessful.</returns>
    public static Result<Customer> CreatePlanCustomer(string code, string name, int cityId, int districtId, int? wardId,
        Guid actionByUser)
    {
        var customer = new Customer(name, actionByUser);
        Result rs = customer.CreatePlanCustomer(code, cityId, districtId, wardId);
        if (rs.IsFailure) return Result.Failure<Customer>(rs.Error);

        return Result.Success(customer);
    }

    /// <summary>
    /// Creates or updates a metric for a customer.
    /// </summary>
    /// <param name="customer">The customer.</param>
    /// <param name="product">The product.</param>
    /// <param name="timeFrame">The time frame.</param>
    /// <param name="lastStockNumber">The last stock number.</param>
    /// <param name="wholeSalesNumber">The whole sales number.</param>
    /// <param name="retailSalesNumber">The retail sales number.</param>
    /// <param name="stockNumber">The stock number.</param>
    /// <returns>A result indicating the success or failure of the operation.</returns>
    public static Result CreateOrUpdateMetric(Customer customer,
        Product product,
        TimeFrame timeFrame,
        double lastStockNumber,
        double wholeSalesNumber,
        double retailSalesNumber,
        double stockNumber)
    {
        Result<CustomerTimeFrame> rsCustomerTimeFrame = customer.FindCustomerTimeFrame(timeFrame);
        if (rsCustomerTimeFrame.IsFailure) return Result.Failure(rsCustomerTimeFrame.Error);

        Result<Metric> rsMetric = rsCustomerTimeFrame.Value.FindMetric(product);
        if (rsMetric.IsFailure) return Result.Failure(rsMetric.Error);

        rsMetric.Value.AddOrUpdateForeCast(product.GetPriceByTimeFrame(timeFrame), lastStockNumber, wholeSalesNumber,
            retailSalesNumber, stockNumber);

        return Result.Success();
    }

    /// <summary>
    /// Calculates the total planned amount for a given customer, time frames, and metric type.
    /// </summary>
    /// <param name="customer">The customer for which to calculate the total planned amount.</param>
    /// <param name="timeFrameIds">The list of time frame IDs for which to calculate the total planned amount.</param>
    /// <param name="eMetricType">The metric type for which to calculate the total planned amount.</param>
    /// <returns>The total planned amount for the given customer, time frames, and metric type.</returns>
    public static double TotalPlannedAmountByTimeFrames(Customer customer,
        List<Guid> timeFrameIds,
        EMetricType eMetricType)
    {
        var metrics = customer.CustomerTimeFrames.Where(x => timeFrameIds.Contains(x.TimeFrameId))
            .SelectMany(x => x.Metrics).ToList();
        if (metrics.Count == 0) return 0;

        double totalPlannedAmount = eMetricType switch
        {
            EMetricType.ForeCast => metrics.Select(x => x.ForeCast?.TotalAmount ?? 0).Sum(),
            EMetricType.Target => metrics.Select(x => x.Target?.TotalAmount ?? 0).Sum(),
            EMetricType.OriginalBudget => metrics.Select(x => x.OriginalBudget?.TotalAmount ?? 0).Sum(),
            _ => 0
        };

        return totalPlannedAmount;
    }
}