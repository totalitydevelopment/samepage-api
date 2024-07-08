using Nok.Core.Events;
using Nok.Core.Extensions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nok.Core.Aggregates;

public abstract class BaseEntity<TId> : BaseAudit, IBaseEntity
{
    private readonly List<DomainEventBase> _domainEvents = [];

    public long ClusterId { get; private set; }

    public TId Id { get; protected set; }

    [NotMapped]
    public IEnumerable<DomainEventBase> DomainEvents => _domainEvents.AsReadOnly();

    public BaseEntity()
    {
        _now = SystemTime.UtcNow();
    }

    protected void RegisterDomainEvent(DomainEventBase domainEvent) => _domainEvents.Add(domainEvent);

    void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    void IBaseEntity.ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    protected void SetUpdatedTracking(Guid updatedBy)
    {
        UpdatedDate = DateTime.UtcNow;
        UpdatedBy = updatedBy;
    }

    protected static DateTimeOffset _now;

}

public interface IBaseEntity
{
    IEnumerable<DomainEventBase> DomainEvents { get; }
    void ClearDomainEvents();
}

public abstract class BaseAudit
{
    public DateTimeOffset CreatedDate { get; protected set; }
    public Guid CreatedBy { get; protected set; }
    public DateTimeOffset UpdatedDate { get; protected set; }
    public Guid UpdatedBy { get; protected set; }
}