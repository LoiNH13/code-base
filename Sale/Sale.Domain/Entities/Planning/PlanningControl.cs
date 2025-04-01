using Domain.Core.Abstractions;
using Domain.Core.Primitives;
using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using Sale.Domain.Core.Errors;
using Sale.Domain.Enumerations;
using Sale.Domain.Events;

namespace Sale.Domain.Entities.Planning;

public sealed class PlanningControl : AggregateRoot, IAuditableEntity, ISoftDeletableEntity
{
    public PlanningControl(string name) : base(Guid.NewGuid())
    {
        Name = name;
        Status = PlanningControlStatus.New;
    }

    public string Name { get; private set; } = string.Empty;

    public PlanningControlStatus Status { get; private set; } = PlanningControlStatus.New;

    public DateTime CreatedOnUtc { get; }

    public DateTime? ModifiedOnUtc { get; }

    public DateTime? DeletedOnUtc { get; }

    public bool Deleted { get; }

    public List<PlanningControlLine> PlanningControlLines { get; } = new();

    private PlanningControl() { }

    public Result Update(string name)
    {
        Name = name;
        return Result.Success();
    }

    public Result AddLine(TimeFrame timeFrame, bool isOriginalBudget, bool isTarget)
    {
        PlanningControlLine line = new PlanningControlLine(this, timeFrame, isOriginalBudget, isTarget);
        PlanningControlLines.Add(line);

        return Result.Success();
    }

    public Result<PlanningControlLine> DeleteLine(Guid planningControlLineId)
    {
        Maybe<PlanningControlLine> mbLine = PlanningControlLines.Find(x => x.Id == planningControlLineId) ?? default!;
        if (mbLine.HasNoValue) return Result.Failure<PlanningControlLine>(SaleDomainErrors.PlanningControlLine.NotFound);
        return Result.Success<PlanningControlLine>(mbLine);
    }

    public Result UpdateStatus(PlanningControlStatus status)
    {
        Status = status;
        if (status == PlanningControlStatus.Completed) AddDomainEvent(new PlanningControlCompletedDomainEvent(Id));
        return Result.Success();
    }
}
