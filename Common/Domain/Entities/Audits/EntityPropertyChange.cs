using Domain.Core.Primitives;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace Domain.Entities.Audits;

public sealed class EntityPropertyChange : Entity
{
    internal EntityPropertyChange(Guid entityChangeId,
                                  string propertyName,
                                  string propertyType,
                                  string? originalValue,
                                  string? newValue)
        : base(Guid.NewGuid())
    {
        EntityChangeId = entityChangeId;
        PropertyName = propertyName;
        PropertyType = propertyType;
        OriginalValue = originalValue;
        NewValue = newValue;
        CreatedOnUtc = DateTime.UtcNow;
    }
    public Guid EntityChangeId { get; private set; }

    public string PropertyName { get; private set; }

    public string PropertyType { get; private set; }

    public string? OriginalValue { get; private set; }

    public string? NewValue { get; private set; }

    public DateTime CreatedOnUtc { get; init; }

    private EntityPropertyChange() { }
}
