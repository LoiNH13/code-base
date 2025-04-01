using Domain.Core.Primitives;

namespace Sale.Domain.Enumerations;

public sealed class PlanningControlStatus : Enumeration<PlanningControlStatus>
{
    public static readonly PlanningControlStatus New = new(1, "Mới");

    public static readonly PlanningControlStatus InProgress = new(2, "Đang Plan");

    public static readonly PlanningControlStatus Completed = new(3, "Đã khóa");

    private PlanningControlStatus(int value, string name) : base(value, name)
    {
    }

    public PlanningControlStatus(int value) : base(value, FromValue(value).Value.Name)
    {
    }
}
