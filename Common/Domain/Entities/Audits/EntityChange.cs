using Domain.Core.Primitives;
using System.Collections.ObjectModel;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace Domain.Entities.Audits;

public sealed class EntityChange : AggregateRoot
{
    public string ChangeType { get; set; }

    public Guid EntityId { get; set; }

    public string EntityName { get; set; }

    public string? Route { get; set; }

    public string? ClientIpAddress { get; set; }

    public string? RequestId { get; set; }

    public Guid? CreateBy { get; set; }

    public DateTime CreatedOnUtc { get; init; }

    public ICollection<EntityPropertyChange> PropertyChanges { get; private set; }

    private EntityChange() { }

    public EntityChange(string changeType, Guid entityId, string entityName)
        : base(Guid.NewGuid())
    {
        ChangeType = changeType;
        EntityId = entityId;
        EntityName = entityName;
        CreatedOnUtc = DateTime.UtcNow;
        PropertyChanges = new Collection<EntityPropertyChange>();
    }

    public void SetUserInfo(string requestId, string route, string? clientIpAddress, string? createBy)
    {
        ClientIpAddress = clientIpAddress;
        RequestId = requestId;
        Route = route;
        if (Guid.TryParse(createBy, out Guid result))
        {
            CreateBy = result;
        }
    }

    public void AddPropertyChange(string propertyName, string propertyType, string? originalValue, string? newValue)
    {
        PropertyChanges.Add(new EntityPropertyChange(Id, propertyName, propertyType, originalValue, newValue));
    }
}
