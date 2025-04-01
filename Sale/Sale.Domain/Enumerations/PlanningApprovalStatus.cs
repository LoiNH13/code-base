using Domain.Core.Primitives;

namespace Sale.Domain.Enumerations;

public sealed class PlanningApprovalStatus : Enumeration<PlanningApprovalStatus>
{
    public static readonly PlanningApprovalStatus WaitingForApproval = new(1, "Đang đợi duyệt");

    public static readonly PlanningApprovalStatus Approved = new(2, "Đã duyệt");

    public static readonly PlanningApprovalStatus EditRequest = new(3, "Yêu cầu chỉnh sửa");

    public static readonly PlanningApprovalStatus Locked = new(4, "Đã khóa");

    private PlanningApprovalStatus(int value, string name) : base(value, name) { }

    public PlanningApprovalStatus(int value) : base(value, FromValue(value).Value.Name) { }
}
