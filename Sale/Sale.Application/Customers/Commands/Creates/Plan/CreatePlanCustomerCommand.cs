using Domain.Core.Primitives.Result;

namespace Sale.Application.Customers.Commands.Creates.Plan;

public sealed class CreatePlanCustomerCommand : ICommand<Result>
{
    public CreatePlanCustomerCommand(int cityId, int districtId, int? wardId)
    {
        CityId = cityId;
        DistrictId = districtId;
        WardId = wardId ?? 0;
    }

    public int CityId { get; }

    public int DistrictId { get; }

    public int? WardId { get; }
}
