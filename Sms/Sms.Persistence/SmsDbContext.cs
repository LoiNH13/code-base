using Application.Core.Abstractions.Common;
using Application.Core.Abstractions.Data;
using Domain.Core.Abstractions;
using Domain.Core.Events;
using Domain.Core.Primitives;
using Domain.Core.Primitives.Maybe;
using Domain.Events;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Persistence.Extensions;
using System.Reflection;

namespace Sms.Persistence;

public class SmsDbContext : DbContext, IDbContext, IUnitOfWork
{
    private readonly IDateTime _dateTime;
    private readonly IMediator _mediator;

    /// <summary>
    ///     Initializes a new instance of the <see cref="SmsDbContext" /> class.
    /// </summary>
    /// <param name="options">The options to be used by a <see cref="DbContext" />.</param>
    /// <param name="dateTime">The service that provides date and time functionality.</param>
    /// <param name="mediator">The mediator for handling domain events.</param>
    public SmsDbContext(DbContextOptions<SmsDbContext> options, IDateTime dateTime, IMediator mediator)
        : base(options)
    {
        _dateTime = dateTime;
        _mediator = mediator;
    }

    #region default functions

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.ApplyUtcDateTimeConverter();

        base.OnModelCreating(modelBuilder);
    }

    /// <inheritdoc />
    public new DbSet<TEntity> Set<TEntity>()
        where TEntity : Entity
    {
        return base.Set<TEntity>();
    }

    /// <inheritdoc />
    public async Task<Maybe<TEntity>> GetBydIdAsync<TEntity>(Guid id)
        where TEntity : Entity
    {
        return id == Guid.Empty
            ? Maybe<TEntity>.None
            : Maybe<TEntity>.From(await Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id) ?? default!);
    }

    /// <inheritdoc />
    public void Insert<TEntity>(TEntity entity)
        where TEntity : Entity
    {
        Set<TEntity>().Add(entity);
    }

    /// <inheritdoc />
    public void InsertRange<TEntity>(IReadOnlyCollection<TEntity> entities)
        where TEntity : Entity
    {
        Set<TEntity>().AddRange(entities);
    }

    /// <inheritdoc />
    public new void Remove<TEntity>(TEntity entity)
        where TEntity : Entity
    {
        Set<TEntity>().Remove(entity);
    }

    /// <inheritdoc />
    public Task<int> ExecuteSqlAsync(string sql, IEnumerable<SqlParameter> parameters,
        CancellationToken cancellationToken = default)
    {
        return Database.ExecuteSqlRawAsync(sql, parameters, cancellationToken);
    }

    /// <inheritdoc />
    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        return Database.BeginTransactionAsync(cancellationToken);
    }

    /// <summary>
    ///     Publishes and then clears all the domain events that exist within the current transaction.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    private async Task PublishDomainEvents(CancellationToken cancellationToken)
    {
        List<EntityEntry<AggregateRoot>> aggregateRoots = ChangeTracker
            .Entries<AggregateRoot>()
            .Where(entityEntry => entityEntry.Entity.DomainEvents.Count != 0)
            .ToList();

        var domainEvents =
            aggregateRoots.SelectMany(entityEntry => entityEntry.Entity.DomainEvents).ToList();

        var domainSyncEvents =
            aggregateRoots.SelectMany(entityEntry => entityEntry.Entity.DomainSyncEvents).ToList();

        aggregateRoots.ForEach(entityEntry => entityEntry.Entity.ClearDomainEvents());

        aggregateRoots.ForEach(entityEntry => entityEntry.Entity.ClearDomainSyncEvents());

        var tasks = domainEvents.Select(domainEvent => _mediator.Publish(domainEvent, cancellationToken));

        foreach (var domainEvent in domainSyncEvents) await _mediator.Publish(domainEvent, cancellationToken);

        await Task.WhenAll(tasks);
    }

    /// <summary>
    ///     Updates the entities implementing <see cref="ISoftDeletableEntity" /> interface.
    /// </summary>
    /// <param name="utcNow">The current date and time in UTC format.</param>
    private void UpdateSoftDeletableEntities(DateTime utcNow)
    {
        foreach (EntityEntry<ISoftDeletableEntity> entityEntry in ChangeTracker.Entries<ISoftDeletableEntity>())
        {
            if (entityEntry.State != EntityState.Deleted) continue;

            entityEntry.Property(nameof(ISoftDeletableEntity.DeletedOnUtc)).CurrentValue = utcNow;

            entityEntry.Property(nameof(ISoftDeletableEntity.Deleted)).CurrentValue = true;

            entityEntry.State = EntityState.Modified;

            UpdateDeletedEntityEntryReferencesToUnchanged(entityEntry);
        }
    }

    /// <summary>
    ///     Updates the specified entity entry's referenced entries in the deleted state to the modified state.
    ///     This method is recursive.
    /// </summary>
    /// <param name="entityEntry">The entity entry.</param>
    private static void UpdateDeletedEntityEntryReferencesToUnchanged(EntityEntry entityEntry)
    {
        if (!entityEntry.References.Any()) return;

        foreach (var targetEntry in entityEntry.References
                     .Where(r => r.TargetEntry?.State == EntityState.Deleted)
                     .Select(x => x.TargetEntry))
        {
            if (targetEntry is null) continue;

            targetEntry.State = EntityState.Unchanged;
            UpdateDeletedEntityEntryReferencesToUnchanged(targetEntry);
        }
    }

    /// <summary>
    ///     Updates the entity based on its state and generates domain events accordingly.
    /// </summary>
    /// <param name="entry">The entity entry to be updated.</param>
    /// <param name="utcNow">The current date and time in UTC format.</param>
    /// <param name="domainEvents">The list of domain events to be populated.</param>
    private static void UpdateEntityBasedOnState(EntityEntry<IAuditableEntity> entry, DateTime utcNow,
        List<IDomainEvent> domainEvents)
    {
        switch (entry.State)
        {
            case EntityState.Added:
                HandleAddedEntity(entry, utcNow);
                domainEvents.Add(CreateDomainEvent(typeof(EntityAddedEvent<>), entry));
                break;
            case EntityState.Modified:
                HandleModifiedEntity(entry, utcNow);
                domainEvents.Add(CreateDomainEvent(typeof(EntityModifiedEvent<>), entry));
                break;
            case EntityState.Deleted:
                domainEvents.Add(CreateDomainEvent(typeof(EntityDeletedEvent<>), entry));
                break;
            case EntityState.Detached:
                break;
            case EntityState.Unchanged:
                break;
            default:
                throw new ArgumentOutOfRangeException("Unhandled EntityState: " + entry.State);
        }
    }

    /// <summary>
    ///     Handles the behavior when an entity is added to the database context.
    /// </summary>
    /// <param name="entry">The entry of the added entity.</param>
    /// <param name="utcNow">The current date and time in UTC format.</param>
    private static void HandleAddedEntity(EntityEntry<IAuditableEntity> entry, DateTime utcNow)
    {
        // Set the created on UTC property of the auditable entity
        entry.Property(nameof(IAuditableEntity.CreatedOnUtc)).CurrentValue = utcNow;
    }

    /// <summary>
    ///     Handles the behavior when an entity is modified in the database context.
    ///     Sets the modified date of the auditable entity to the current date and time.
    /// </summary>
    /// <param name="entry">The entry of the modified entity.</param>
    /// <param name="utcNow">The current date and time in UTC format.</param>
    private static void HandleModifiedEntity(EntityEntry<IAuditableEntity> entry, DateTime utcNow)
    {
        entry.Property(nameof(IAuditableEntity.ModifiedOnUtc)).CurrentValue = utcNow;
    }


    /// <summary>
    ///     Creates a domain event for the specified entity.
    /// </summary>
    /// <param name="eventType">The type of the domain event.</param>
    /// <param name="entityEntry">The entity entry.</param>
    /// <returns>The domain event.</returns>
    private static IDomainEvent CreateDomainEvent(Type eventType, EntityEntry<IAuditableEntity> entityEntry)
    {
        // Use Activator.CreateInstance to create an instance of the specified domain event type,
        // passing the entity as a parameter to the constructor.
        // The MakeGenericType method is used to create a generic type from the eventType.
        // The entity type is obtained from the entityEntry.
        // The '!' symbol is used to assert that the result of Activator.CreateInstance will never be null.
        return (IDomainEvent)Activator.CreateInstance(eventType.MakeGenericType(entityEntry.Entity.GetType()),
            entityEntry.Entity)!;
    }

    /// <summary>
    ///     Updates the entities implementing <see cref="IAuditableEntity" /> interface.
    /// </summary>
    /// <param name="utcNow">The current date and time in UTC format.</param>
    /// <param name="cancellationToken">cancellationToken</param>
    private async Task UpdateAuditableEntities(DateTime utcNow, CancellationToken cancellationToken)
    {
        List<EntityEntry<IAuditableEntity>> auditableEntries = ChangeTracker.Entries<IAuditableEntity>()
            .Where(e => e.State != EntityState.Unchanged)
            .ToList();

        List<IDomainEvent> domainEvents = new();

        foreach (EntityEntry<IAuditableEntity> entityEntry in auditableEntries)
            UpdateEntityBasedOnState(entityEntry, utcNow, domainEvents);
        //check entity changed here example at SaleDbContext
        IEnumerable<Task> tasks = domainEvents.Select(domainEvent => _mediator.Publish(domainEvent, cancellationToken));
        await Task.WhenAll(tasks);
    }

    #endregion

    /// <summary>
    ///     Saves all the pending changes in the unit of work.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of entities that have been saved.</returns>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var utcNow = _dateTime.UtcNow;

        await PublishDomainEvents(cancellationToken);

        await UpdateAuditableEntities(utcNow, cancellationToken);

        UpdateSoftDeletableEntities(utcNow);

        return await base.SaveChangesAsync(cancellationToken);
    }
}